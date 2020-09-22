using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerState : State
{
    public PowerState(Game game) : base(game) { }

    public override void Tick()
    {
        if (game.powerbar.OutOfRange())
        {
            game.powerbar.Reset();
            game.GetCursor().Enable();
            game.SetState(new IdleState(game));
        }
        else
        {
            game.powerbar.Tick();
            // TODO - update wind here?
        }
    }

    public override void OnKeySpace()
    {
        game.powerbar.SetPower();
        game.SetState(new AccuracyState(game));
    }
}
