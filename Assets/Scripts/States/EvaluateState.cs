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
            game.SetState(new PostHoleState(game));
            return;
        }
        else if (game.ball.InWater())
        {
            game.IncrementStrokes();
            // TODO - reset the ball
        }
        game.SetState(new PrepareState(game));
    }
}
