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

    public override void OnKeyLeftArrow()
    {
        Vector3 p = ball.GetPosition();
        ball.SetPosition(p.x, p.y, p.z + INCREMENT);
    }

    public override void OnKeyRightArrow()
    {
        Vector3 p = ball.GetPosition();
        ball.SetPosition(p.x, p.y, p.z - INCREMENT);
    }

    public override void OnKeyUpArrow()
    {
        Vector3 p = ball.GetPosition();
        ball.SetPosition(p.x + INCREMENT, p.y, p.z);
    }

    public override void OnKeyDownArrow()
    {
        Vector3 p = ball.GetPosition();
        ball.SetPosition(p.x - INCREMENT, p.y, p.z);
    }

    public override void OnKeyW()
    {
        Vector3 p = ball.GetPosition();
        ball.SetPosition(p.x, p.y + INCREMENT, p.z);
    }

    public override void OnKeyS()
    {
        Vector3 p = ball.GetPosition();
        ball.SetPosition(p.x, p.y - INCREMENT, p.z);
    }
}
