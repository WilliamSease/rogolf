using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateState : State
{
    public EvaluateState(Game game) : base(game) { }

    public override void Tick()
    {
        // TODO - if (game.ball.InHole())
        if (true)
        {
            // TODO - go to next hole
            game.SetState(new PostHoleState(game));
        }
        // TODO - else if (game.ball.InWater())
        else if (true)
        {
            game.IncrementStrokes();
            // TODO - reset the ball
        }
        // TODO - update wind?
        game.SetState(new PrepareState(game));
    }
}
