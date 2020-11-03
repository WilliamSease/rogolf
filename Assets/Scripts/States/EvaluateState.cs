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
        }
        else
        {
            int shotScore = game.GetScore().AddShotScore();
            UnityEngine.Debug.Log(shotScore);

            if (ball.InWater())
            {
                game.GetHoleBag().GetCurrentHoleData().IncrementStrokes();
                // TODO - reset the ball
            }

            game.SetState(new PrepareState(game));
        }
    }
}
