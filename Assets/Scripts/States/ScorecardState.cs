using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorecardState : State
{
    public ScorecardState(Game game) : base(game) { }

    public override void OnStateEnter()
    {
        // TODO - display the scoreboard
    }

    public override void OnStateExit()
    {
        // TODO - hide the scoreboard
    }

    public override void Tick() { }

    public override void OnKeyReturn()
    {
        // if some end condition, we go to some game over screen

        // else
        game.SetState(new ItemState(game));
    }
}
