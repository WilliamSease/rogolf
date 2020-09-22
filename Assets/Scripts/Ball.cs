using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Ball
{
    private const float ANGLE_INCREMENT = 1/32f;
    private const float SPIN_RATE = 3f;
    private const float SPIN_DECAY = 0.5f;
    private const float NO_HEIGHT_TIME_OUT = 5f;

    private Game game;
    private float deltaTime;
    private float noHeightTime;

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
        gravity = new Vector3(0, -9.8f, 0);
        radius = 0.0625f;
        c = 0.5f;
        rho = 1.2f;
        A = Mathf.PI * Mathf.Pow(radius, 2);

        dtheta = 0;
        height = 0;
        terrainNormal = new Vector3(Single.NaN, Single.NaN, Single.NaN);

        Reset(new Vector3(0,0,0));
    }

    public void Reset(Vector3 v)
    {
        position = new Vector3(v.x, v.y, v.z);
        SetLastPosition();
        velocity = new Vector3(0,0,0);
        fnet = gravity * mass;

        angle = 0;
        dtheta = 0;

        spin = new Vector3(0,0,0);
    }

    public void Reset() { Reset(new Vector3(0,0,0)); }

    public void Strike(Club club, float power, float accuracy)
    {
        float clubPower = club.GetPower() * power;
        float clubLoft = club.GetLoft();

        SetLastPosition();

        // Set velocity
        float horizontal = clubPower * Mathf.Cos(clubLoft);
        float vertical = clubPower * Mathf.Sin(clubLoft);
        Vector3 angleVector = VectorUtil.FromPolar(horizontal * mass, angle);
        velocity = angleVector;
        velocity.y = vertical;

        // Set wind resistance
        fnet = gravity * mass;
        // Set spin
        Vector2 clubVector = VectorUtil.FromPolar(club.GetPower(), club.GetLoft());
        spin = VectorUtil.FromPolar(-clubVector.y / clubVector.x * SPIN_RATE, angle);
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
            outputString.Append(club.GetPower() + "," + club.GetLoft() + "\nx,y\n");
            maxHeight = 0;
        }

        Reset();
        Strike(club, 1f, 0f);
        terrainNormal = new Vector3(0,1,0);
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
    public void FindTrajectory(Club club, float distanceTarget, float maxHeightTarget)
    {
        Tuple<float,float,string> result;
        float distance = Single.NaN;
        float maxHeight = Single.NaN;
        string outputString = "";
        for (int i = 0; i < 1000; i++)
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
        System.IO.File.WriteAllText(club.GetName() + ".csv", distance + "," + maxHeight + "\n" + outputString.ToString());
    }

    public void Tick()
    {
        //UnityEngine.Debug.Log(position + "\t" + velocity + "\t" + height + "\t" + noHeightTime); // TODO - debug
        
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
        VectorUtil.Rotate(velocity, dtheta);
        // Apply wind
        //velocity += 
        // Update wind resistance
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
            //position.y -= height;
            velocity *= GetFriction(debug);
        }
    }

    private float GetBounce(bool debug)
    {
        return debug ? TeeTerrain.BOUNCE : game.GetTerrainAttributes().GetBounce(terrainHit);
    }

    private float GetFriction(bool debug)
    {
        return debug ? TeeTerrain.FRICTION : game.GetTerrainAttributes().GetFriction(terrainHit);
    }

    public void AngleToHole()
    {
        Vector3 holePosition = game.GetHoleInfo().GetHolePosition();
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

    public bool InAir() { return height > 0.001; }
    public bool InMotion() { return velocity.magnitude > 0.25; } // This is wrong because it isn't adjusted for deltaTime
    public bool IsMoving() { return InAir() || InMotion(); }

    public bool InHole() { return position == game.GetHoleInfo().GetHolePosition(); }
    public bool InWater() { return false; } // TODO

    public void SetDeltaTime() { this.deltaTime = Time.deltaTime * rate; }
    public void SetRate(float rate) { this.rate = rate; }
    public void SetInaccuracyRate(float inaccuracyRate) { this.inaccuracyRate = inaccuracyRate; }
    public void SetPosition(Vector3 v) { position = new Vector3(v.x, v.y, v.z); }
    public void SetPosition(float x, float y, float z) { position = new Vector3(x, y, z); } 
    public void SetLastPosition() { lastPosition = new Vector3(position.x, position.y, position.z); }

    public Vector3 GetPosition() { return new Vector3(position.x, position.y, position.z); }
    public float GetAngle() { return angle; }
}
