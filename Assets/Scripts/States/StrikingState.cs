using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikingState : State
{
    public StrikingState(Game game) : base(game) { }

    public override void Tick()
    {
        // Hit the damn thing
        game.GetBall().Strike(game.GetBag().GetClub(), (float)game.GetPowerbar().GetPower(), 0f);
        
        game.GetHoleBag().GetCurrentHoleData().IncrementStrokes();
        game.SetState(new RunningState(game));
    }
}
