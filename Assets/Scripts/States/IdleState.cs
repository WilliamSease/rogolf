using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Game game) : base(game) { }

    public override void Tick() {
        game.GetCursor().Tick();
    }

    public override void OnKeySpace()
    {
        game.GetPowerbar().Reset();
        game.SetState(new PowerState(game));
    }

    public override void OnKeyUpArrow() { game.GetBag().DecrementBag(); }
    public override void OnKeyW() { game.GetBag().DecrementBag(); }
    public override void OnKeyDownArrow() { game.GetBag().IncrementBag(); }
    public override void OnKeyS() { game.GetBag().IncrementBag(); }

    public override void OnKeyLeftArrow() { game.GetBall().IncrementAngle(); }
    public override void OnKeyA() { game.GetBall().IncrementAngle(); }
    public override void OnKeyRightArrow() { game.GetBall().DecrementAngle(); }
    public override void OnKeyD() { game.GetBall().DecrementAngle(); }

    public override void OnKeyE() {
        GameController gc = game.GetGameController();
        gc.ToggleGreenNormalMap();
    }
}
