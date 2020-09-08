using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private GameObject ball;

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
        //UnityEngine.Debug.Log(ball.GetComponent<Rigidbody>().velocity.magnitude);
        //if (ball.GetComponent<Rigidbody>().velocity.magnitude == 0)
        ball.GetComponent<Rigidbody>().velocity = new Vector3(100, 100, 100);
    }

    public override void OnKeyLeftArrow()
    {
        //ball.transform.Rotate(Time.deltaTime * 0.25);
    }
}
