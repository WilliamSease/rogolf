using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : State
{
    public RunningState(Game game) : base(game) { }

    public override void Tick()
    {
        if (game.ball.IsMoving())
        {
            game.ball.Tick();
            game.currentDistance.Tick();
        }
        else
        {
            game.SetState(new PostShotState(game)) ;
        }
    }
}
