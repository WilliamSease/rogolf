using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind
{
    // Hellmann Exponent
    public const float a = 0.27f;

    private const float MAX_INITIAL_SPEED = 0.5f;
    private const float SPEED_RATE = 0.1f;
    private const float ANGLE_RATE = 1f;
    private const float RATIO = 1f/256f;

    private Game game;

    private float speed;
    private float angle;
    private Vector3 wind;

    public Wind(Game game)
    {
        this.game = game;
        Reset();
    }

    public void Reset()
    {
        speed = UnityEngine.Random.Range(0.0f, MAX_INITIAL_SPEED);
        angle = UnityEngine.Random.Range(0.0f, Mathf.PI*2f);
        UpdateVector();
    }

    public void Disable()
    {
        speed = 0f;
        angle = 0f;
        UpdateVector();
    }

    public void Increment()
    {
        speed += UnityEngine.Random.Range(0.0f, 1.0f) * SPEED_RATE - SPEED_RATE / 2;
        angle += UnityEngine.Random.Range(0.0f, 1.0f) * ANGLE_RATE - ANGLE_RATE / 2;

        // Ensure speed >= 0
        speed = speed >= 0 ? speed : 0;

        UpdateVector();
    }

    private void UpdateVector()
    {
        wind = MathUtil.FromPolar(speed, angle);
    }

    public Vector3 GetWindVector(float height) {
        return (height > 0 && height != Single.NaN) ? wind * (RATIO * Mathf.Pow(height / 10, Wind.a)) : Vector3.zero;
    }

    public void SetSpeed(float speed) { 
        this.speed = speed;
        UpdateVector();
    }
    public void SetAngle(float angle) {
        this.angle = angle;
        UpdateVector();
    }

    public float GetSpeed() { return speed; }
    public float GetAngle() { return angle; }
}
