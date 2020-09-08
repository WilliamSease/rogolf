using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostShotState : State
{
    private const double TIME_TOTAL = 0.5;
    private double timeRemaining;

    public PostShotState(Game game) : base(game) {
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
            game.SetState(new EvaluateState(game));
        }
    }
}
