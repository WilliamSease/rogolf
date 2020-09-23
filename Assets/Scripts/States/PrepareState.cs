using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareState : State
{
    public PrepareState(Game game) : base(game) { }

    public override void Tick()
    {
        game.GetCursor().Enable();
        game.GetBall().AngleToHole();
        game.GetBag().SelectBestClub();
        game.getFreeFocus().transform.position = game.GetBall().GetPosition();
        // TODO - update the wind value
        game.SetState(new IdleState(game));
    }
}
