using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareState : State
{
    public PrepareState(Game game) : base(game) { }

    public override void Tick()
    {
        game.GetCursorGraphics().Enable();
        game.GetBall().AngleToHole();
        game.GetBag().SelectBestClub();
        game.GetShotMode().Validate();
        game.GetFreeFocus().transform.position = game.GetBall().GetPosition();
        game.GetPowerbar().Reset();
        // TODO - update the wind value
        game.SetState(new IdleState(game));
    }
}
