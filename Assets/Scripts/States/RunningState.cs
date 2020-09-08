using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : State
{
    public RunningState(Game game) : base(game) { }

    public override void Tick()
    {
        // TODO - update wind?
        // TODO - if (game.ball.IsMoving())
        if (false)
        {
            // TODO - game.ball.Tick();
            // TODO - game.currentYards.Tick();
        }
        else
        {
            game.SetState(new PostShotState(game)) ;
        }
    }
}
