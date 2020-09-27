using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyState : State
{
    private Powerbar powerbar;

    public AccuracyState(Game game) : base(game) {
        this.powerbar = game.GetPowerbar();
    }

    public override void Tick()
    {
        powerbar.Tick();

        if (powerbar.OutOfRange())
        {
            powerbar.SetAccuracy();
            game.SetState(new PreStrikingState(game));
        }
    }

    public override void OnKeySpace()
    {
        if (powerbar.ValidAccuracy())
        {
            powerbar.SetAccuracy();
            game.SetState(new PreStrikingState(game));
        }
    }
}
