using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostHoleState : State
{
    private const double TIME_TOTAL = 2.0;
    private double timeRemaining;

    public PostHoleState(Game game) : base(game) {
        Reset();
    }

    public void Reset()
    {
        timeRemaining = TIME_TOTAL;
    }

    public override void OnStateEnter()
    {
        int payout = (MathUtil.Payouts(MathUtil.Intify(game.GetHoleBag().GetCurrentHoleData().GetStrokes().ToString()), 
                        MathUtil.Intify(game.GetHoleInfo().GetPar().ToString()))); ///AAAAAAHH I swear this works.
        game.PayPlayer(payout);
        GameObject.Find("UICanvas").GetComponent<GodOfUI>().ShowVictory(payout);
    }

    public override void OnStateExit()
    {
        GameObject.Find("UICanvas").GetComponent<GodOfUI>().HideVictory();
    }

    public override void Tick()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            Reset();
            game.gc.EndHole();
        }
    }
}
