using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : State
{
    private Ball ball;
    private CurrentDistance currentDistance;

    public RunningState(Game game) : base(game) {
        this.ball = game.GetBall();
        this.currentDistance = game.GetCurrentDistance();
    }

    public override void Tick()
    {
        if (ball.IsMoving())
        {
            ball.Tick();
            currentDistance.Tick();
        }
        else
        {
            game.SetState(new PostShotState(game)) ;
        }
    }
}
