﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGraphics
{
    private Game game;
    private Ball ball;
    private MouseOrbitImproved orbitalControls;
    private GameObject ballObject;

    public BallGraphics(Game game)
    {
        this.game = game;
        ball = game.GetBall();
        orbitalControls = game.orbitalControls;
        ballObject = game.GetBallObject();
    }

    public void Tick()
    {
        Vector3 position = ball.GetPosition();
        float cameraDistance = orbitalControls.distance;
        float radius = ball.GetRadius();

        // Update ball graphical position
        ballObject.transform.localPosition = new Vector3(position.x, position.y + radius, position.z);
        
        // Update ball graphical size
        if (cameraDistance > 5) radius *= Mathf.Pow(cameraDistance, 0.75f);
        ballObject.transform.localScale = new Vector3(radius, radius, radius);
    }
}
