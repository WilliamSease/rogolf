using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostHoleState : State
{
    private const double TIME_TOTAL = 2.0;
    private double timeRemaining;

    public PostHoleState(Game game) : base(game) {
        Reset();
    }

    public void Reset()
    {
        timeRemaining = TIME_TOTAL;
    }

    public override void OnStateEnter()
    {
        // TODO - This is where you'd display 'Par' or 'Birdie' in the middle of the screen
    }

    public override void OnStateExit()
    {
        // TODO - hide everything that we just created
    }

    public override void Tick()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            Reset();
            game.SetState(new ScorecardState(game));
        }
    }
}
