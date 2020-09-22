using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareState : State
{
    public PrepareState(Game game) : base(game) { }

    public override void Tick()
    {
        // TODO - do stuff before each shot
        game.GetBall().AngleToHole();
        // TODO - update the wind value
        game.SetState(new IdleState(game));
    }
}
