using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private const float INCREMENT = 2; // TODO - debug for moving camera

    private Ball ball;

    public IdleState(Game game) : base(game)
    {
        this.ball = game.ball;
    }

    public override void Tick()
    {
        // TODO do gamey stuff here
    }

    public override void OnKeySpace()
    {
        game.powerbar.Reset();
        game.SetState(new PowerState(game));
    }

    public override void OnKeyUpArrow() { game.GetBag().DecrementBag(); }
    public override void OnKeyDownArrow() { game.GetBag().IncrementBag(); }
    public override void OnKeyW() { game.GetBag().DecrementBag(); }
    public override void OnKeyS() { game.GetBag().IncrementBag(); }

}
