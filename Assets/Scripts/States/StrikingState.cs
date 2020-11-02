using ShotModeEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikingState : State
{
    public StrikingState(Game game) : base(game) { }

    public override void Tick()
    {
        Mode shotMode = game.GetShotMode().GetShotMode();
        // Hit the damn thing
        game.GetBall().Strike(shotMode, game.GetBag().GetClub(), (float) game.GetPowerbar().GetPower(), (float) game.GetPowerbar().GetAccuracy());

        game.GetShotMode().Strike();
        game.GetHoleBag().GetCurrentHoleData().IncrementStrokes();
        game.SetState(new RunningState(game));
    }
}
