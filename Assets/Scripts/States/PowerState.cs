using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerState : State
{
    private Powerbar powerbar;

    public PowerState(Game game) : base(game) {
        this.powerbar = game.GetPowerbar();
    }

    public override void Tick()
    {
        if (powerbar.OutOfRange())
        {
            powerbar.Reset();
            game.GetCursorGraphics().Enable();
            game.SetState(new IdleState(game));
        }
        else
        {
            powerbar.Tick();
        }
    }

    public override void OnKeySpace()
    {
        powerbar.SetPower();
        game.SetState(new AccuracyState(game));
    }
}
