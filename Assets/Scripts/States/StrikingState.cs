using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikingState : State
{
    public StrikingState(Game game) : base(game) { }

    public override void Tick()
    {
        // TODO - strike ball
        // TODO - update wind?
        game.IncrementStrokes();
        game.SetState(new RunningState(game));
    }
}
