using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateState : State
{
    public EvaluateState(Game game) : base(game) { }

    public override void Tick()
    {
        if (game.ball.InHole())
        {
            // TODO - go to next hole
            game.SetState(new PostHoleState(game));
        }
        else if (game.ball.InWater())
        {
            game.IncrementStrokes();
            // TODO - reset the ball
        }
        game.SetState(new PrepareState(game));
    }
}
