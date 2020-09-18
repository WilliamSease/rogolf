using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikingState : State
{
    public StrikingState(Game game) : base(game) { }

    public override void Tick()
    {
        Club currentClub = game.GetBag().GetClub();
        // TODO factor in accuracy during striking
        float power = currentClub.GetPower() * (float)game.GetPowerbar().GetPower();
        game.GetBall().Strike(power, currentClub.GetLoft(), 0);
        game.IncrementStrokes();
        game.SetState(new RunningState(game));
    }
}
