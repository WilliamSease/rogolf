using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind
{
    private const float MAX_INITIAL_SPEED = 0.5f;
    private const float SPEED_RATE = 0.1f;
    private const float ANGLE_RATE = 1f;
    private const float HEIGHT_RATE = 1f/128f;
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
        speed = Random.Range(0.0f, MAX_INITIAL_SPEED);
        angle = Random.Range(0.0f, Mathf.PI*2f);
        wind = VectorUtil.FromPolar(speed, angle);
    }

    public void Disable()
    {
        speed = 0f;
        angle = 0f;
        wind = VectorUtil.FromPolar(speed, angle);
    }

    /// <summary>
    /// Currently unused.
    /// </summary>
    public void UpdateWind()
    {
        speed += Random.Range(0.0f, 1.0f) * SPEED_RATE - SPEED_RATE / 2;
        angle += Random.Range(0.0f, 1.0f) * ANGLE_RATE - ANGLE_RATE / 2;

        // Ensure speed >= 0
        speed = speed >= 0 ? speed : 0;

        // Recalculate wind vector
        wind = VectorUtil.FromPolar(speed, angle);
    }

    public void SetSpeed(float speed) { this.speed = speed; }
    public void SetAngle(float angle) { this.angle = angle; }

    public float GetSpeed() { return speed; }
    public float GetAngle() { return angle; }
    public Vector3 GetWindVector() { return wind * RATIO; }
}
