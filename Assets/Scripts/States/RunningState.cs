using System;
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

    public override void OnStateEnter()
    {
        godOfUI.renderPowerbar = false;
    }

    public override void Tick()
    {
        if (ball.IsRunning())
        {
            try
            {
                ball.Tick();
                currentDistance.Tick();
            }
            catch (OutOfBounds e)
            {
                // Reset ball and add penalty stroke
                ball.Reset();
                game.GetHoleBag().GetCurrentHoleData().IncrementStrokes();
            }
        }
        else
        {
			godOfUI.renderPowerbar = true;
            game.SetState(new PostShotState(game)) ;
        }
    }
}
