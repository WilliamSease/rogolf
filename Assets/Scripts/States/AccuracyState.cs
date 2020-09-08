using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyState : State
{
    public AccuracyState(Game game) : base(game) { }

    public override void Tick()
    {
        game.powerbar.Tick();
        // TODO - update wind?

        if (game.powerbar.OutOfRange())
        {
            game.powerbar.SetAccuracy();
            game.SetState(new PreStrikingState(game));
        }
    }

    public override void OnKeySpace()
    {
        if (game.powerbar.ValidAccuracy())
        {
            game.powerbar.SetAccuracy();
            game.SetState(new PreStrikingState(game));
        }
    }
}
