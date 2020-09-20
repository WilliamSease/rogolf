using System;
using System.Collections;
using System.Collections.Generic;
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
    private Vector3 lastPosition;

    private float rate;
    private float inaccuracyRate;
    private float mass;
    private Vector3 gravity;
    private float friction;
    private float bounce;
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
        friction = 1.0f;
        bounce = 0.5f;
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

    /// <summary>
    /// Strikes the ball given power, loft, and inaccuracy angle.
    /// Calculate attributes and ball lie before you call this.
    /// </summary>
    public void Strike(float power, float loft, float dtheta)
    {
        SetLastPosition();

        // Set velocity
        float horizontal = power * Mathf.Cos(loft);
        float vertical = power * Mathf.Sin(loft);
        Vector3 angleVector = VectorUtil.FromPolar(horizontal * mass, angle);
        velocity = angleVector;
        velocity.y = vertical;

        // Set wind resistance
        fnet = gravity * mass;
        // Set spin
        Club c = game.GetBag().GetClub();
        Vector2 clubVector = VectorUtil.FromPolar(c.GetPower(), c.GetLoft());
        spin = VectorUtil.FromPolar(-clubVector.y / clubVector.x * SPIN_RATE, angle);
        // Set inaccuracy
        this.dtheta = dtheta * inaccuracyRate; 
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
        }
        else if (Physics.Raycast(new Ray(positionHigh, Vector3.down), out hit))
        {
            noHeightTime = 0;
            height = position.y - hit.point.y;
            terrainNormal = hit.normal;
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

    private void CalculateBounce()
    {
        if (height <= 0)
        {
            position.y -= height;
            velocity = Vector3.Reflect(velocity, terrainNormal);
            velocity.y *= bounce;

            // Calculate spin
            //velocity += spin;
            spin *= SPIN_DECAY;
        }
    }

    private void CalculateFriction()
    {
        if (!InAir())
        {
            //position.y -= height;
            velocity *= friction * 0.95f;
        }
    }

    public void IncrementAngle()
    {
        UnityEngine.Debug.Log(ANGLE_INCREMENT);
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
    public bool InMotion() { return velocity.magnitude > 0.25; }
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
}
