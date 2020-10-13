using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Ball
{
    private const float ANGLE_INCREMENT = 1/64f;
    private const float SPIN_RATE = 4.5f;
    private const float SPIN_DECAY = 0.5f;
    private const float NO_HEIGHT_TIME_OUT = 5f;
    private const float INITIAL_MINIMUM_VELOCITY_THRESHOLD = 0.1f;
    private const float FINAL_MINIMUM_VELOCITY_THRESHOLD = 0.005f;
    private const float GRAVITATIONAL_ACCELERATION = 9.8f;
    private const float CUP_DEPTH = 0.12f;

    private Game game;
    private Wind wind;
    private float deltaTime;
    private float noHeightTime;
    private float lie;

    private Vector3 position;
    private Vector3 velocity;
    private Vector3 fnet;
    private float angle;
    private float dtheta;
    private Vector3 spin;
    private float height;
    private Vector3 terrainNormal;
    private RaycastHit terrainHit;
    private bool hasBounced;
    private Vector3 lastPosition;
    private Vector3 holePosition;

    private float rate;
    private float inaccuracyRate;
    private float mass;
    private Vector3 gravity;
    private float radius;
    private float c;
    private float rho;
    private float A;

    private float cupEffectMagnitude;
    private float cupEffectRadius;
    private Vector3 cupEffect;
    private float cupLipRadius;
    private float cupRadius;

    public Ball(Game game)
    {
        this.game = game;
        this.wind = game.GetWind();
        deltaTime = 0;
        noHeightTime = 0;

        // Initialize default parameters
        rate = 1f;
        inaccuracyRate = 3.0E-5f;
        mass = 0.25f;
        gravity = new Vector3(0, -GRAVITATIONAL_ACCELERATION, 0);
        radius = 0.0625f;
        c = 0.5f;
        rho = 1.2f;
        A = Mathf.PI * Mathf.Pow(radius, 2);

        cupEffectMagnitude = 1E-5f;
        cupEffectRadius = 0.2f;
        cupRadius = 0.108f;

        dtheta = 0;
        height = 0;
        terrainNormal = MathUtil.Vector3NaN;

        Reset();
    }

    public void Reset(Vector3 v)
    {
        position = new Vector3(v.x, v.y, v.z);
        SetLastPosition();
        velocity = Vector3.zero;
        fnet = gravity * mass;

        angle = 0;
        dtheta = 0;

        spin = Vector3.zero;
    }

    public void Reset() { Reset(Vector3.zero); }

    public void Strike(Club club, float power, float accuracy, bool debug = false)
    {
        // Get player attributes
        PlayerAttributes playerAttributes = game.GetPlayerAttributes();

        // Get terrain info
        if (!debug) SetHeight();

        // Set power
        lie = !debug ? GetTerrainType().GetLie() : 1f;
        float clubPower = club.GetPower() * power * Mathf.Lerp(0.5f, 1.0f, playerAttributes.GetPower() * lie);
        float clubLoft = club.GetLoft();

        SetLastPosition();

        // Apply control
        //angle += TODO

        // Set velocity
        float horizontal = clubPower * Mathf.Cos(clubLoft);
        float vertical = clubPower * Mathf.Sin(clubLoft);
        Vector3 angleVector = MathUtil.FromPolar(horizontal * mass, angle);
        velocity = angleVector;
        velocity.y = vertical;

        // Account for terrain slope
        // TODO - rotate velocity vector?

        // Set drag
        fnet = gravity * mass;

        // Set spin
        Vector3 clubVector = MathUtil.FromPolar(club.GetPower(), club.GetLoft());
        spin = MathUtil.FromPolar(-clubVector.z / clubVector.x * SPIN_RATE * Mathf.Lerp(0.5f, 2.0f, playerAttributes.GetSpin()), angle);

        // Set inaccuracy
        dtheta = accuracy * inaccuracyRate * Mathf.Lerp(1.5f, 0.5f, playerAttributes.GetImpact());

        // Set other
        hasBounced = false; 
    }

    public Tuple<float,float,string> SimulateDistance(Club club, bool debug = false)
    {
        float carry = 0;
        bool set = false;

        StringBuilder outputString = new StringBuilder();
        float maxHeight = 0;
        if (debug)
        {
            outputString.Append(String.Format("Power:,{0}\nLoft:,{1}\nMass:,{2}\nRadius:,{3}\nx,y\n", club.GetPower(), club.GetLoft(), mass, radius));
            maxHeight = 0;
        }

        Reset();
        Strike(club, 1f, 0f, true);
        terrainNormal = Vector3.up;
        // Set holePosition to be unreachable
        holePosition = Vector3.down;
        while (true)
        {
            deltaTime = 0.03f;
            if (IsMoving())
            {
                if (debug)
                {
                    outputString.Append(position.x + "," + position.y  + "\n");
                    maxHeight = Math.Max(maxHeight, position.y);
                }
                UpdatePhysicsVectors();
                height = position.y;
                CalculateBounce(true);
                CalculateFriction(true);
                if (!set && hasBounced)
                {
                    carry = position.magnitude;
                    set = true;
                }
            }
            else { break; }
        }
        club.SetDistance(position.magnitude);

        return new Tuple<float,float,string>(carry, maxHeight, outputString.ToString());
    }

    /// <summary>
    /// Debug method for brute-forcing the correct trajectory.
    /// </summary>
    public void FindTrajectory(Club club, float distanceTarget, float maxHeightTarget, int iterations, int clubIndex)
    {
        Tuple<float,float,string> result;
        float distance = Single.NaN;
        float maxHeight = Single.NaN;
        string outputString = "";
        for (int i = 0; i < iterations; i++)
        {
            result = SimulateDistance(club, true);
            distance = result.Item1;
            maxHeight = result.Item2;
            outputString = result.Item3;

            if (distance == distanceTarget)
            {
                if (maxHeight == maxHeightTarget)
                {
                    break;
                }
                else
                {
                    club.SetLoft(club.GetLoft() + 0.001f*Math.Sign(maxHeightTarget - maxHeight));
                }
            }
            else
            {
                if (maxHeight == maxHeightTarget)
                {
                    club.SetPower(club.GetPower() + 1f*Math.Sign(distanceTarget - distance));
                }
                else
                {
                    club.SetPower(club.GetPower() + 1f*Math.Sign(distanceTarget - distance));
                    club.SetLoft(club.GetLoft() + 0.001f*Math.Sign(maxHeightTarget - maxHeight));
                }
            }
        }
        string filename = String.Format("{0}-{1}-{2}.csv", DateTime.Now.ToString("yyyyMMddTHHmmss"), clubIndex, club.GetName());
        string info = String.Format("Distance:,{0}y\nMax Height:,{1}y\n", MathUtil.ToYards(distance), MathUtil.ToYards(maxHeight));
        System.IO.File.WriteAllText(filename, info + outputString.ToString());
    }

    public void Tick()
    {
        SetDeltaTime();
        if (IsMoving())
        {
            UpdatePhysicsVectors();
            ApplyWind();
            SetHeight();
            CalculateBounce();
            CalculateFriction();
        }
        else
        {
            Reset(position);
        }
    }

    private void UpdatePhysicsVectors()
    {
        // Update position
        position += velocity * (deltaTime / mass);
        // Apply inaccuracy - TODO
        //if (!hasBounced) velocity = MathUtil.Rotate(velocity, dtheta/deltaTime);
        // Update drag
        fnet = (gravity * mass) - ((velocity * (0.5f*c*rho*A * Mathf.Pow(velocity.magnitude / mass, 2))) / velocity.magnitude);
        fnet *= deltaTime;
        // Update velocity
        velocity += fnet;
    }

    public void ApplyWind()
    {
        velocity += wind.GetWindVector(height);
    }

    private void SetHeight()
    {
        RaycastHit hit;
        Vector3 positionHigh = new Vector3(position.x, position.y + 1000, position.z);
        if (Physics.Raycast(new Ray(position, Vector3.down), out hit))
        {
            noHeightTime = 0;
            height = position.y - hit.point.y;
            terrainNormal = hit.normal;
            terrainHit = hit;
        }
        else if (Physics.Raycast(new Ray(positionHigh, Vector3.down), out hit))
        {
            noHeightTime = 0;
            height = position.y - hit.point.y;
            terrainNormal = hit.normal;
            terrainHit = hit;
        }
        else
        {
            if (noHeightTime < NO_HEIGHT_TIME_OUT)
            {
                noHeightTime += deltaTime;
                height = Single.PositiveInfinity;
            }
            else
            {
                throw new InvalidOperationException("Ball height not found");
            }
        }
    }

    // TODO
    private bool CalculateCup()
    {   
        float distanceToHole = DistanceToHole();
        // If ball is in or right above the cup
        if (distanceToHole < cupRadius)
        {
            //cupEffect = cupEffectMagnitude/deltaTime * Vector3.Normalize(new Vector3(holePosition.x, holePosition.y-CUP_DEPTH, holePosition.z) - position);
            //velocity += cupEffect;
            return true;
        }
        // If ball is in cup effect radius
        else if (distanceToHole < cupEffectRadius)
        {
            //cupEffect = cupEffectMagnitude/deltaTime * Vector3.Normalize(holePosition - position);
            //velocity += cupEffect;
            return false;
        }
        // If ball is outside AoE
        else { 
            cupEffect = Vector3.zero;
            return false;
        }
    }

    private void CalculateBounce(bool debug = false)
    {
        bool onCup = false;
        if (!debug) { onCup = CalculateCup(); }

        if (height <= 0)
        {
            if (onCup && !debug) CupBounce();
            else
            {
                position.y -= height;
                velocity = Vector3.Reflect(velocity, terrainNormal);
                velocity.y *= GetBounce(debug);

                // Calculate spin
                velocity += spin;
                spin *= SPIN_DECAY;

                hasBounced = true;
            }
        }
    }

    // TODO
    private void CupBounce()
    {
        if (height < 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(new Ray(position, velocity), out hit))
            {
                UnityEngine.Debug.Log(hit.transform.gameObject.name);
                float cupFaceDistance = position.y - hit.point.y;
                if (cupFaceDistance < 0.01f)
                {
                    velocity = 0.5f * Vector3.Reflect(velocity, terrainNormal);
                }
            }
        }
        // If at bottom of cup, zero velocity
        if (height <= -CUP_DEPTH * 0.75)
        {
            velocity = Vector3.zero;
        }
    }

    private void CalculateFriction(bool debug = false)
    {
        if (!InAir())
        {
            // N = mgcos(theta)
            // normal_force = mass * gravity * vertical_component
            float normalForce = mass * GRAVITATIONAL_ACCELERATION * terrainNormal.y;
            // f_k = C*N
            // kinetic_friction = friction_coef * (normal_force * -unit_velocity)
            Vector3 frictionForce = GetFriction(debug) * (-normalForce * Vector3.Normalize(MathUtil.Copy(velocity)));
            // If ball 'overcomes' friction
            if (velocity.magnitude > INITIAL_MINIMUM_VELOCITY_THRESHOLD) { velocity += frictionForce; }
            else { velocity += (velocity.magnitude > (frictionForce*100f).magnitude) ? frictionForce*100f : -velocity; }
        }
    }

    private float GetBounce(bool debug)
    {
        return debug ? TerrainAttributes.SIMULATED_BOUNCE : game.GetTerrainAttributes().GetBounce(terrainHit);
    }

    private float GetFriction(bool debug)
    {
        return debug ? TerrainAttributes.SIMULATED_FRICTION : game.GetTerrainAttributes().GetFriction(terrainHit);
    }
    
    public void AngleToHole()
    {
        angle = Mathf.Atan2(holePosition.z - position.z, holePosition.x - position.x);
    }

    public void IncrementAngle()
    {
        angle += ANGLE_INCREMENT;
        if (angle >= Mathf.PI * 2.0f)
        {
            angle -= Mathf.PI * 2.0f;
        }
    }

    public void DecrementAngle()
    {
        angle -= ANGLE_INCREMENT;
        if (angle <= 0)
        {
            angle += Mathf.PI * 2.0f;
        }
    }

    public Tuple<float, float> GetTerrainAngle() { return MathUtil.GetTerrainAngle(terrainNormal, angle); }

    // TODO - public bool InAir() { return Vector3.Dot(Vector3.Normalize(MathUtil.Copy(velocity)), terrainNormal) > Mathf.PI / 24f; }
    public bool InAir() { return height > 0.01f; }
    public bool InMotion() { return velocity.magnitude > FINAL_MINIMUM_VELOCITY_THRESHOLD; }
    public bool IsMoving() { return InAir() || InMotion(); }

    public float DistanceToHole() { return Vector3.Distance(position, holePosition); }
    public TerrainType GetTerrainType() {
        try { return game.GetTerrainAttributes().GetTerrainType(terrainHit); }
        catch { return game.GetTerrainAttributes().GetTeeTerrain(); }
    }
    public bool InHole() { return DistanceToHole() < CUP_DEPTH && height <= -CUP_DEPTH+0.025f; }
    public bool OnGreen() { 
        try { return game.GetTerrainAttributes().OnGreen(terrainHit); }
        catch { return false; };
    }
    public bool InWater() { return game.GetTerrainAttributes().InWater(terrainHit); }

    public void SetDeltaTime() { this.deltaTime = Time.deltaTime * rate; }
    public void SetRate(float rate) { this.rate = rate; }
    public void SetInaccuracyRate(float inaccuracyRate) { this.inaccuracyRate = inaccuracyRate; }
    public void SetPosition(Vector3 v) { position = MathUtil.Copy(v); }
    public void SetPosition(float x, float y, float z) { position = new Vector3(x, y, z); }
    public void SetRelativePosition(Vector3 v) { position = position + v; }
    public void SetRelativePosition(float x, float y, float z) { SetRelativePosition(new Vector3(x, y, z)); }
    public void SetLastPosition() { lastPosition = MathUtil.Copy(position); }
    public void SetHolePosition() { holePosition = game.GetHoleInfo().GetHolePosition(); }
    public void SetMass(float mass) { this.mass = mass; }
    public void SetRadius(float radius) { this.radius = radius; }

    public float GetLie() { return lie; }

    public Vector3 GetPosition() { return MathUtil.Copy(position); }
    public Vector3 GetLastPosition() { return lastPosition; }
    public float GetAngle() { return angle; }
    public float GetMass() { return mass; }
    public float GetRadius() { return radius; }

    // Debug
    public Vector3 GetVelocity() { return velocity; }
    public Vector3 GetFnet() { return fnet; }
    public float GetHeight() { return height; }
    public Vector3 GetCupEffect() { return cupEffect; }
}
