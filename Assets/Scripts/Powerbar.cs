using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerbar
{
    public const float NEGATIVE_PART = 0.2f;
    public const float BAR_MIN = -NEGATIVE_PART;
    private const float RATE = 1f;

    Game game;

    private float current;
    private Direction direction;
    private float power;
    private float accuracy;

    public enum Direction
    {
        DECREASING,
        IDLE,
        INCREASING
    }

    public Powerbar(Game game)
    {
        this.game = game;
        Reset();
    }

    public void Reset()
    {
        current = 0f;
        direction = Direction.INCREASING;
        power = 0f;
        accuracy = 0f;
    }

    public void Tick()
    {
        float time = Time.deltaTime * RATE;

        if (direction == Direction.INCREASING)
        {
            if (current < 1)
            {
                current += time;
            }
            else
            {
                direction = Direction.DECREASING;
            }
        }
        else if (direction == Direction.DECREASING)
        {
            if (current > BAR_MIN)
            {
                current -= time;
            }
            else
            {
                accuracy = BAR_MIN;
            }
        }
    }

    public bool OutOfRange() { return current <= BAR_MIN; }
    public bool ValidAccuracy() { return current <= -BAR_MIN; }

    public void SetPower() { power = current; }
    public void SetAccuracy() { accuracy = current; }
    public float GetCurrent() { return current; }
    public float GetPower() { return power; }
    public float GetAccuracyInternal() { return accuracy; }
    public float GetAccuracy() { return accuracy / -BAR_MIN; }
}
