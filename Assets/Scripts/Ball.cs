using MaterialTypeEnum;
using ShotModeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Ball
{
    private const float ANGLE_INCREMENT = 1/64f;
    private const float SPIN_RATE = 4.5f;
    private const float SPIN_DECAY = 0.5f;
    private const float NO_HEIGHT_TIME_OUT = 5f;
    private const float INITIAL_MINIMUM_VELOCITY_THRESHOLD = 0.03f;
    private const float FINAL_MINIMUM_VELOCITY_THRESHOLD = 0.001f;
    private const float GRAVITATIONAL_ACCELERATION = 9.8f;
    private const float CUP_DEPTH = 0.14f;
    private const float CUP_BOUNCE = 0.5f;

    private Game game;
    private Wind wind;
    private float deltaTime = 0;
    private float noHeightTime = 0;
    private float lie;

    // Ball information
    private Vector3 position;
    private Vector3 velocity;
    private Vector3 fnet;
    private float angle;
    private float dtheta = 0;
    private Vector3 spin;
    private float height = 0;
    private Vector3 terrainNormal = MathUtil.Vector3NaN;
    private RaycastHit terrainHit;
    private RaycastHit cupHit;
    private bool hasBounced;
    private Vector3 lastPosition;
    private Vector3 holePosition;

    // Physics parameters
    private float rate = 1f;
    private float inaccuracyRate = 1.0E-1f;
    private float mass = 0.04593f;
    private Vector3 gravity = new Vector3(0, -GRAVITATIONAL_ACCELERATION, 0);
    private float radius = 0.021335f;
    private float c = 0.5f;
    private float rho = 1.2f;
    private float A;

    // Cup parameters
    private bool wasOnCup = false;
    private bool onCup = false;
    private bool inHole = false;
    private float cupEffectMagnitude = 1E-5f;
    private float cupEffectRadius = 0.2f;
    private Vector3 cupEffect;
    private float cupLipRadius;
    private float cupRadius = 0.11f;

    public Ball(Game game)
    {
        this.game = game;
        this.wind = game.GetWind();

        A = Mathf.PI * Mathf.Pow(radius, 2);

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

        inHole = false;
        wasOnCup = false;
    }

    public void Reset() { Reset(Vector3.zero); }

    public void Strike(Mode shotMode, Club club, float power, float accuracy, bool debug = false)
    {
        // Get player attributes
        PlayerAttributes playerAttributes = game.GetPlayerAttributes();

        // Get terrain info
        if (!debug) SetHeight();

        // Set power
        lie = !debug ? GetTerrainType().GetLie() : 1f;
        float clubPower = club.GetPower() * power * Mathf.Lerp(0.5f, 1.0f, playerAttributes.GetPower() * lie);
        float clubLoft = club.GetLoft();

        // Adjust for shot mode
        switch (shotMode)
        {
            case Mode.NORMAL:
                break;
            case Mode.POWER:
                clubPower *= 1.25f;
                break;
            case Mode.APPROACH:
                clubPower *= 0.5f;
                break;
            default:
                throw new Exception(String.Format("Unsupported shot mode {0}", shotMode));
        }

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
        inHole = false;
        wasOnCup = false;
    }

    public void Tick()
    {
        SetDeltaTime();
        if (IsMoving() || !InHole())
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
        // Apply inaccuracy
        if (!hasBounced) velocity = MathUtil.Rotate(velocity, dtheta * deltaTime);
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
        bool hasHit = Physics.Raycast(new Ray(position, Vector3.down), out hit);
        bool hitCup = hasHit ? hit.transform.gameObject.name[0] == 'C' : false;
        if (hasHit && !hitCup)
        {
            noHeightTime = 0;
            height = position.y - hit.point.y;
            terrainNormal = hit.normal;
            terrainHit = hit;
        }
        else
        {
            RaycastHit[] hits = Physics.RaycastAll(new Ray(positionHigh, Vector3.down));
            // TODO - debug
            //string[] ss = (from h in hits select h.transform.gameObject.name).ToArray();
            //UnityEngine.Debug.Log(string.Join(", ", ss));
            if (hits.Length > 0)
            {
                hit = hits[0];
                noHeightTime = 0;
                height = position.y - hit.point.y;
                terrainNormal = hit.normal;
                terrainHit = hit;
                foreach (RaycastHit h in hits)
                    if (h.transform.gameObject.name[0] == 'C')
                        cupHit = h;
            }
            else
            {
                if (noHeightTime < NO_HEIGHT_TIME_OUT)
                {
                    noHeightTime += deltaTime;
                    height = Single.PositiveInfinity;
                }
                else throw new InvalidOperationException("Ball height not found");
            }
        }
    }

    // TODO
    private bool CalculateCup()
    {   
        float distanceToHole = DistanceToHole();
        // If ball is in or right above the cup
        if ((distanceToHole < cupRadius && height <= 0) || (distanceToHole < CUP_DEPTH && height <= -cupRadius))
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
        else
        { 
            cupEffect = Vector3.zero;
            return false;
        }
    }

    private void CalculateBounce(bool debug = false)
    {
        wasOnCup = onCup;
        onCup = false;
        if (!debug) { onCup = CalculateCup(); }

        if (onCup && !debug) { OnCupBounce(); }
        else if (wasOnCup && !debug) { WasOnCupBounce(); }
        else if (height <= 0)
        {
            if (IsRolling())
            {
                position.y -= height;
                velocity = velocity.magnitude * GetGroundVector();
            }
            else
            {
                position.y -= height;
                velocity = Vector3.Reflect(velocity, terrainNormal);
                velocity.y *= GetBounce(debug);
            }

            // Calculate spin
            velocity += spin;
            spin *= SPIN_DECAY;

            hasBounced = true;
        }
    }

    private void OnCupBounce()
    {
        // TODO - see ball go in hole
        terrainHit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        game.GetGameController().GetCupHole().GetComponent<MeshRenderer>().enabled = false;

        RaycastHit hit;
        if (Physics.Raycast(new Ray(position, velocity), out hit))
        {
            float cupFaceDistance = Vector3.Distance(position, hit.point);
            /*
            UnityEngine.Debug.Log(cupFaceDistance.ToString());
            if (cupFaceDistance < 0.01f)
            {
                velocity = 0.5f * Vector3.Reflect(velocity, hit.normal);
            }
            */
            velocity = Vector3.zero;
            position = position + new Vector3(0, -CUP_DEPTH/2f, 0);
            inHole = true;
        }
        else UnityEngine.Debug.Log("Hit not found");
    }

    private void WasOnCupBounce()
    {
        //velocity = Vector3.zero;
        //velocity = CUP_BOUNCE * Vector3.Reflect(velocity, new Vector3(holePosition.x, position.y, holePosition.z));
        //onCup = true;
    }

    private void CalculateFriction(bool debug = false)
    {
        if (IsRolling())
        {
            // N = mgcos(theta)
            // normal_force = mass * gravity * vertical_component
            float normalForce = mass * GRAVITATIONAL_ACCELERATION * terrainNormal.y;
            // f_k = C*N
            // kinetic_friction = friction_coef * (normal_force * -unit_velocity)
            Vector3 frictionForce = GetFriction(debug) * (-normalForce * Vector3.Normalize(MathUtil.Copy(velocity)));
            frictionForce *= deltaTime;
            // If ball 'overcomes' friction
            if (velocity.magnitude > INITIAL_MINIMUM_VELOCITY_THRESHOLD) { velocity += frictionForce; }
            else { velocity += (velocity.magnitude > (frictionForce*5f).magnitude) ? frictionForce*5f : -velocity; }
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
    public float GetVelocityAngle() { return MathUtil.GetVelocityAngle(terrainNormal, velocity); }

    public bool IsRolling() { return !InAir() && GetVelocityAngle() < MathUtil.RadsToDeg(Mathf.PI / 10f); }
    public bool InAir() { return height > 0.005f; }
    public bool InMotion() { return velocity.magnitude > FINAL_MINIMUM_VELOCITY_THRESHOLD; }
    public bool IsMoving() { return InAir() || InMotion(); }

    public bool IsRunning() { return onCup ? !InHole() : IsMoving(); }

    public float DistanceToHole() { return Vector3.Distance(position, holePosition); }
    public float DistanceFromTee() { return Vector3.Distance(position, game.GetHoleInfo().GetTeePosition()); }

    public MaterialType GetMaterialType()
    {
        try { return game.GetTerrainAttributes().GetMaterialType(terrainHit); }
        catch { return MaterialType.NONE; }
    }

    public TerrainType GetTerrainType()
    {
        try { return game.GetTerrainAttributes().GetTerrainType(terrainHit); }
        catch { return game.GetTerrainAttributes().GetTeeTerrain(); }
    }

    //public bool InHole() { return DistanceToHole() <= CUP_DEPTH-0.01f && height < -CUP_DEPTH * 0.75; }
    public bool InHole() { return inHole; }
    public bool OnGreen()
    { 
        try { return game.GetTerrainAttributes().OnGreen(terrainHit); }
        catch { return false; };
    }
    public bool InWater() { return game.GetTerrainAttributes().InWater(terrainHit); }

    public void SetDeltaTime() { this.deltaTime = Time.deltaTime * rate; }
    public void SetRate(float rate) { this.rate = rate; }
    public void SetInaccuracyRate(float inaccuracyRate) { this.inaccuracyRate = inaccuracyRate; }
    public void SetPosition(Vector3 v) { position = MathUtil.Copy(v); }
    public void SetPosition(float x, float y, float z) { UnityEngine.Debug.Log("set position"); position = new Vector3(x, y, z); }
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
    public Vector3 GetGroundVector() { return MathUtil.GetGroundVector(terrainNormal, velocity); }

    #region Simulation
    public void SimulateDistances(Club club)
    {
        SimulateDistance(Mode.NORMAL, club);
        SimulateDistance(Mode.POWER, club);
        SimulateDistance(Mode.APPROACH, club);
    }

    public Tuple<float,float,string> SimulateDistance(Mode shotMode, Club club, bool debug = false)
    {
        Vector3 oldPosition = position;
        Vector3 oldHolePosition = holePosition;

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
        Strike(shotMode, club, 1f, 0f, true);
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
        club.SetDistance(shotMode, position.magnitude);

        Reset(oldPosition);
        holePosition = oldHolePosition;

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
            result = SimulateDistance(Mode.NORMAL, club, true);
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

    private string SimulateFramerate(Club club, float dt)
    {
        List<Tuple<Vector3, Vector3, Vector3, float>> output = new List<Tuple<Vector3, Vector3, Vector3, float>>();

        Reset();
        Strike(Mode.NORMAL, club, 1f, 0f, true);
        terrainNormal = Vector3.up;
        // Set holePosition to be unreachable
        holePosition = Vector3.down;
        while (true)
        {
            deltaTime = dt;
            if (IsMoving())
            {
                UpdatePhysicsVectors();
                height = position.y;
                CalculateBounce(true);
                CalculateFriction(true);

                output.Add(new Tuple<Vector3, Vector3, Vector3, float>(velocity/dt, fnet/dt, spin/dt, dtheta/dt));
            }
            else { break; }
        }
        
        StringBuilder sb = new StringBuilder();
        foreach (Tuple<Vector3, Vector3, Vector3, float> item in output)
        {
            sb.Append(String.Format("{0},{1},{2},{3}\n",
                    MathUtil.Vector3ToString(item.Item1), MathUtil.Vector3ToString(item.Item2), MathUtil.Vector3ToString(item.Item3), item.Item4.ToString()));
        }
        return sb.ToString();
    }

    public void TestFramerateTies()
    {
        Club club = game.GetBag().GetClub();

        string outputSlow = SimulateFramerate(club, 0.05f);
        string outputFast = SimulateFramerate(club, 0.005f);

        string date = DateTime.Now.ToString("yyyyMMddTHHmmss");
        System.IO.File.WriteAllText(String.Format("{0}-{1}-{2}.csv", date, "framerateTest", "slow"), outputSlow);
        System.IO.File.WriteAllText(String.Format("{0}-{1}-{2}.csv", date, "framerateTest", "fast"), outputFast);
    }
    #endregion
}
