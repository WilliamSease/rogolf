using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateState : State
{
    private Ball ball;

    public EvaluateState(Game game) : base(game) {
        this.ball = game.GetBall();
    }

    public override void Tick()
    {
        if (ball.InHole())
        {
            game.SetState(new PostHoleState(game));
            return;
        }
        else if (ball.InWater())
        {
            game.GetHoleBag().GetCurrentHoleData().IncrementStrokes();
            // TODO - reset the ball
        }
        //game.GetScore().AddShotCredits();
        game.SetState(new PrepareState(game));
    }
}
