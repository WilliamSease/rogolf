using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball
{
    private const double ANGLE_INCREMENT = 1/32;

    private Game game;
    private float time;

    private Vector3 position;
    private Vector3 velocity;
    private Vector3 fnet;
    private double angle;
    private double dtheta;
    private Vector3 spin;
    private Vector3 lastPosition;

    private float rate;
    private float inaccuracyRate;
    private float mass;
    private Vector3 gravity;
    private double friction;
    private double bounce;
    private double radius;
    private double c;
    private double rho;
    private double A;

    public Ball(Game game)
    {
        this.game = game;
        time = 0;

        // Initialize default parameters
        rate = 750f;
        inaccuracyRate = 1/128f;
        mass = 0.25f;
        gravity = new Vector3(0, -9.8f, 0);
        friction = 1;
        bounce = -0.5;
        radius = 0.0625;
        c = 0.5;
        rho = 1.2;
        A = Math.PI * Math.Pow(radius, 2);

        Reset(new Vector3(0,0,0));
    }

    public void Reset(Vector3 v)
    {
        position = new Vector3(v.x, v.y, v.z);
        // TODO - setLastPosition
        velocity = new Vector3(0,0,0);
        // TODO - fnet = gravity.multiply(mass)

        angle = 0;
        dtheta = 0;

        spin = new Vector3(0,0,0);
    }

    public void Strike(Club club, double dtheta)
    {
        // TODO - set last position
        // TODO - set velocity
        // TODO - set wind resistance
        // TODO - set spin
        // TODO - set innacuracy
    }

    public void Tick()
    {
        SetDeltaTime(Time.deltaTime);
        if (IsMoving())
        {
            // Update position
            position += velocity * (time / mass);
            // Apply inaccuracy
            // TODO
            // Apply wind
            // TODO
            // Update wind resistance
            // TODO
            // Update velocity
            // TODO

            // Calculate bounce
            // TODO
            // Calculate friction
            // TODO
        }
        else
        {
            Reset(position);
        }
    }

    public void IncrementAngle()
    {
        angle += ANGLE_INCREMENT;
        if (angle >= Math.PI * 2)
        {
            angle -= Math.PI * 2;
        }
    }

    public void DecrementAngle()
    {
        angle -= ANGLE_INCREMENT;
        if (angle <= 0)
        {
            angle += Math.PI * 2;
        }
    }

    public bool InAir() { return false; } // TODO change this
    public bool InMotion() { return velocity.magnitude > 0.25; }
    public bool IsMoving() { return InAir() || InMotion(); }

    public void SetDeltaTime(float time) { this.time = time /= rate; }
    public void SetRate(float rate) { this.rate = rate; }
    public void SetInaccuracyRate(float innacuracyRate) { this.inaccuracyRate = inaccuracyRate; }
    public void SetPosition(Vector3 v) { position = new Vector3(v.x, v.y, v.z); }
    public void SetPosition(float x, float y, float z) { position = new Vector3(x, y, z); } 
    public void SetLastPosition() { lastPosition = new Vector3(position.x, position.y, position.z); }

    public Vector3 GetPosition() { return new Vector3(position.x, position.y, position.z); }
}
