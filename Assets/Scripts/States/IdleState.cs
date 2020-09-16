using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    //private GameObject ball;
    private Ball ball;

    public IdleState(Game game) : base(game)
    {
        //this.ballObject = game.ballObject;
        this.ball = game.ball;
    }

    public override void Tick()
    {
        Vector3 position = ball.GetPosition();
        //UnityEngine.Debug.Log(position.x + "\t" + position.y + "\t" + position.z);
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
        ball.SetPosition(p.x, p.y - 10, p.z);
    }

    public override void OnKeyRightArrow()
    {
        Vector3 p = ball.GetPosition();
        ball.SetPosition(p.x, p.y + 10, p.z);
    }

    public override void OnKeyUpArrow()
    {
        Vector3 p = ball.GetPosition();
        ball.SetPosition(p.x + 10, p.y, p.z);
    }

    public override void OnKeyDownArrow()
    {
        Vector3 p = ball.GetPosition();
        ball.SetPosition(p.x - 10, p.y, p.z);
    }

    public override void OnKeyW()
    {
        Vector3 p = ball.GetPosition();
        ball.SetPosition(p.x, p.y, p.z + 10);
    }

    public override void OnKeyS()
    {
        Vector3 p = ball.GetPosition();
        ball.SetPosition(p.x, p.y, p.z - 10);
    }
}
