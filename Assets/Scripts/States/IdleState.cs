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
            game.GetCursor().Disable();

            game.GetPowerbar().Reset();
            game.SetState(new PowerState(game));
        }
        else
        {
            game.ToggleTarget();
        }
    }

    public override void OnKeyW() { game.GetBag().DecrementBag(); }
    public override void OnKeyS() { game.GetBag().IncrementBag(); }

    public override void OnKeyA() { game.GetBall().IncrementAngle(); }
    public override void OnKeyD() { game.GetBall().DecrementAngle(); }
	
	private int freeMoveScalar = 200;
    public override void OnKeyUpArrow() //All this is rotated 90deg, wonder why.
	{ 
		game.getFreeFocus().transform.Translate(
			new Vector3(-game.GetCameraObject().transform.right.x, 0, 
				-game.GetCameraObject().transform.right.z)*freeMoveScalar*Time.deltaTime); 
	}
    public override void OnKeyDownArrow() 
	{ 
		game.getFreeFocus().transform.Translate(
			new Vector3(game.GetCameraObject().transform.right.x, 0, 
				game.GetCameraObject().transform.right.z)*freeMoveScalar*Time.deltaTime);
	}
    public override void OnKeyLeftArrow() 
	{ 
		game.getFreeFocus().transform.Translate(
			new Vector3(-game.GetCameraObject().transform.forward.x, 0, 
				-game.GetCameraObject().transform.forward.z)*freeMoveScalar*Time.deltaTime);
	}
    public override void OnKeyRightArrow() 
	{ 
		game.getFreeFocus().transform.Translate(
			new Vector3(game.GetCameraObject().transform.forward.x, 0, 
				game.GetCameraObject().transform.forward.z)*freeMoveScalar*Time.deltaTime);
	}
    
    public override void OnKeyE() { game.GetGameController().ToggleGreenNormalMap(); }

    public override void OnKeyQ() { game.ToggleTarget(); }
}
