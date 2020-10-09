using System.Collections;
using System.Collections.Generic;
using TargetEnum;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Game game) : base(game) { }

    public override void Tick() {
        game.GetCursor().Tick();
    }

    public override void OnKeySpace()
    {
        if (game.GetTarget() == Target.BALL)
        {
            game.GetCursorGraphics().Disable();

            game.GetPowerbar().Reset();
            game.SetState(new PowerState(game));
        }
        else
        {
            game.ResetTarget();
        }
    }

    public override void OnKeyW() { game.GetBag().DecrementBag(); }
    public override void OnKeyS() { game.GetBag().IncrementBag(); }

    public override void OnKeyA() { game.GetBall().IncrementAngle(); }
    public override void OnKeyD() { game.GetBall().DecrementAngle(); }
    
    public override void OnKeyUpArrow() { game.GetBag().DecrementBag(); }
    public override void OnKeyDownArrow() { game.GetBag().IncrementBag(); }

    public override void OnKeyLeftArrow() { game.GetBall().IncrementAngle(); }
    public override void OnKeyRightArrow() { game.GetBall().DecrementAngle(); }
    
    public override void OnKeyE() { game.GetGameController().ToggleGreenNormalMap(); }

    public override void OnKeyQ() { game.ToggleTarget(); }
}
