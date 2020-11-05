using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvaluateState : State
{
    private Ball ball;

    public EvaluateState(Game game) : base(game) {
        this.ball = game.GetBall();
    }

    public override void Tick()
    {
        if (ball.InHole())
        {
            game.SetState(new PostHoleState(game));
        }
        else
        {
            // TODO - debug
            List<Tuple<string, int>> scoreInfo = game.GetScore().AddShotScore();
            string s = string.Join(", ", 
                    (from item in scoreInfo select String.Format("{0}: {1}", item.Item1, item.Item2.ToString())));
            GodOfUI gui = GameObject.Find("UICanvas").GetComponent<GodOfUI>();
            gui.WriteBonus(s);
            gui.InvokeBonus(.5f);
            UnityEngine.Debug.Log(s);

            int shotScore = (from item in scoreInfo select item.Item2).Sum();
            game.GetScore().AddCredit(shotScore);

            if (ball.InWater())
            {
                game.GetHoleBag().GetCurrentHoleData().IncrementStrokes();
                // TODO - reset the ball
            }

            game.SetState(new PrepareState(game));
        }
    }
}
