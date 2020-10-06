using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerbar
{
    private const double RATE = 1;
    private const double BAR_MIN = -0.12;

    Game game;

    private double current;
    private Direction direction;
    private double power;
    private double accuracy;

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
        current = 0;
        direction = Direction.INCREASING;
        power = 0;
        accuracy = 0;
    }

    public void Tick()
    {
        double time = Time.deltaTime * RATE;

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
    public double GetCurrent() { return current; }
    public double GetPower() { return power; }
    public double GetAccuracyInternal() { return accuracy; }
    public double GetAccuracy() { return accuracy / -BAR_MIN; }
}
