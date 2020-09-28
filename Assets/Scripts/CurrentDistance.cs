using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentDistance
{
    private Game game;
    private Ball ball;

    private float distance;

    public CurrentDistance(Game game)
    {
        this.game = game;
        this.ball = game.GetBall();
        distance = 0;
    }

    public void Tick()
    {
        distance = VectorUtil.MapDistance(ball.GetLastPosition(), ball.GetPosition());
    }

    public float GetCurrentDistance() { return distance; }
}
