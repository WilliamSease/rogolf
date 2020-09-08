using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreStrikingState : State
{
    private const double TIME_TOTAL = 1.0;
    private double timeRemaining;

    public PreStrikingState(Game game) : base(game)
    {
        Reset();
    }

    public void Reset()
    {
        timeRemaining = TIME_TOTAL;
    }

    public override void Tick()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            Reset();
            game.SetState(new StrikingState(game));
        }
    }
}
