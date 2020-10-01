using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Ball
{
    private const float ANGLE_INCREMENT = 1/64f;
    private const float SPIN_RATE = 3f;
    private const float SPIN_DECAY = 0.5f;
    private const float NO_HEIGHT_TIME_OUT = 5f;
    private const float INITIAL_MINIMUM_VELOCITY_THRESHOLD = 0.1f;
    private const float FINAL_MINIMUM_VELOCITY_THRESHOLD = 0.005f;
    private const float GRAVITATIONAL_ACCELERATION = 9.8f;

    private Game game;
    private float deltaTime;
    private float noHeightTime;

    private Vector3 position;
    private Vector3 velocity;
    private Vector3 wind;
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

    public Ball(Game game)
    {
        this.game = game;
        deltaTime = 0;
        noHeightTime = 0;

        // Initialize default parameters
        rate = 4/3f;
        inaccuracyRate = 1/128f;
        mass = 0.25f;
        gravity = new Vector3(0, -GRAVITATIONAL_ACCELERATION, 0);
        radius = 0.0625f;
        c = 0.5f;
        rho = 1.2f;
        A = Mathf.PI * Mathf.Pow(radius, 2);

        dtheta = 0;
        height = 0;
        terrainNormal = MathUtil.Vector3NaN;

        Reset(Vector3.zero);
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

    public void Strike(Club club, float power, float accuracy)
    {
        float clubPower = club.GetPower() * power;
        float clubLoft = club.GetLoft();

        SetLastPosition();

        // Set velocity
        float horizontal = clubPower * Mathf.Cos(clubLoft);
        float vertical = clubPower * Mathf.Sin(clubLoft);
        Vector3 angleVector = MathUtil.FromPolar(horizontal * mass, angle);
        velocity = angleVector;
        velocity.y = vertical;

        // Set wind
        wind = game.GetWind().GetWindVector();

        // Set drag
        fnet = gravity * mass;

        // Set spin
        Vector2 clubVector = MathUtil.FromPolar(club.GetPower(), club.GetLoft());
        spin = MathUtil.FromPolar(-clubVector.y / clubVector.x * SPIN_RATE, angle);
        // Set inaccuracy
        this.dtheta = dtheta * inaccuracyRate; 
    }

    public Tuple<float,float,string> SimulateDistance(Club club, bool debug = false)
    {
        hasBounced = false;
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
        Strike(club, 1f, 0f);
        // Overwrite wind vector
        wind = Vector3.zero;
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
        // Apply inaccuracy
        MathUtil.Rotate(velocity, dtheta);
        // Apply wind
        // TODO - velocity += wind * Mathf.Pow(height / 10, Wind.a);
        velocity += wind;
        // Update drag
        fnet = (gravity * mass) - ((velocity * (0.5f*c*rho*A * Mathf.Pow(velocity.magnitude / mass, 2))) / velocity.magnitude);
        // Update velocity
        velocity += fnet * deltaTime;
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

    private void CalculateBounce(bool debug = false)
    {
        if (height <= 0)
        {
            position.y -= height;
            velocity = Vector3.Reflect(velocity, terrainNormal);
            velocity.y *= GetBounce(debug);

            // Calculate spin
            //velocity += spin;
            spin *= SPIN_DECAY;

            hasBounced = true;
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

    // TODO - public bool InAir() { return Vector3.Dot(Vector3.Normalize(MathUtil.Copy(velocity)), terrainNormal) > Mathf.PI / 24f; }
    public bool InAir() { return height > 0.1f; }
    public bool InMotion() { return velocity.magnitude > FINAL_MINIMUM_VELOCITY_THRESHOLD; }
    public bool IsMoving() { return InAir() || InMotion(); }

    public float DistanceToHole() { return Vector3.Distance(position, holePosition); }
    public TerrainType GetTerrainType() {
        try { return game.GetTerrainAttributes().GetTerrainType(terrainHit); }
        catch { return game.GetTerrainAttributes().GetTeeTerrain(); }
    }
    public bool InHole() { return DistanceToHole() < 1; } // TODO - this isn't right
    public bool OnGreen() { 
        try { return game.GetTerrainAttributes().OnGreen(terrainHit); }
        catch { return false; };
    }
    public bool InWater() { return game.GetTerrainAttributes().InWater(terrainHit); }

    public void SetDeltaTime() { this.deltaTime = Time.deltaTime * rate; }
    public void SetRate(float rate) { this.rate = rate; }
    public void SetInaccuracyRate(float inaccuracyRate) { this.inaccuracyRate = inaccuracyRate; }
    public void SetPosition(Vector3 v) { position = new Vector3(v.x, v.y, v.z); }
    public void SetPosition(float x, float y, float z) { position = new Vector3(x, y, z); }
    public void SetRelativePosition(Vector3 v) { position = position + v; }
    public void SetRelativePosition(float x, float y, float z) { SetRelativePosition(new Vector3(x, y, z)); }
    public void SetLastPosition() { lastPosition = new Vector3(position.x, position.y, position.z); }
    public void SetHolePosition() { holePosition = game.GetHoleInfo().GetHolePosition(); }
    public void SetMass(float mass) { this.mass = mass; }
    public void SetRadius(float radius) { this.radius = radius; }

    public Vector3 GetPosition() { return new Vector3(position.x, position.y, position.z); }
    public Vector3 GetLastPosition() { return lastPosition; }
    public float GetAngle() { return angle; }
    public float GetMass() { return mass; }
    public float GetRadius() { return radius; }
}
