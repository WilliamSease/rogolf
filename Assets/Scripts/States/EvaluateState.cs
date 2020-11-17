using GameModeEnum;
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
        switch (game.GetGameMode())
        {
            case GameMode.ROGOLF:
                TickRogolf();
                break;
            case GameMode.RANGE:
                TickRange();
                break;
            default:
                throw new Exception("Unsupported game mode");
        }
    }

    public void TickRogolf()
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
            gui.InvokeBonus(1f);//this is in seconds
            //UnityEngine.Debug.Log(s);

            int shotScore = (from item in scoreInfo select item.Item2).Sum();
            game.GetScore().AddCredit(shotScore);

            if (ball.InWater())
            {
                ball.Reset();
                game.GetHoleBag().GetCurrentHoleData().IncrementStrokes();
                // TODO - reset the ball
            }

            game.SetState(new PrepareState(game));
        }
    }

    public void TickRange()
    {
        ball.Reset();
        game.SetState(new PrepareState(game));
    }
}
