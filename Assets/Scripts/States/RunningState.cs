using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : State
{
    private Ball ball;
    private CurrentDistance currentDistance;
	private GodOfUI godOfUI;

    public RunningState(Game game) : base(game) {
        this.ball = game.GetBall();
        this.currentDistance = game.GetCurrentDistance();
		this.godOfUI = GameObject.Find(GodOfUI.NAME).GetComponent<GodOfUI>();
    }

    public override void Tick()
    {
		godOfUI.renderPowerbar = false;
        if (ball.IsRunning())
        {
            ball.Tick();
            currentDistance.Tick();
        }
        else
        {
			godOfUI.renderPowerbar = true;
            game.SetState(new PostShotState(game)) ;
        }
    }
}
