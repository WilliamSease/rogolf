using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicDebug
{
    private const float SCALE = 2.5E3f;

    private Game game;
    private Ball ball;
    private Wind wind;

    private bool enabled;

    private LineRenderer velocityLine;
    private LineRenderer windLine;
    private LineRenderer cupEffectLine;
    private LineRenderer fnetLine;

    public GraphicDebug(Game game)
    {
        this.game = game;
        this.ball = game.GetBall();
        this.wind = game.GetWind();

        enabled = false;

        velocityLine = CreateLine("VelocityDebug", 0.125f, Color.red);
        windLine = CreateLine("WindDebug", 0.25f, Color.blue);
        cupEffectLine = CreateLine("CupEffectDebug", 0.25f, Color.green);
        fnetLine = CreateLine("FnetDebug", 0.25f, Color.white);
    }

    public LineRenderer CreateLine(string name, float width, Color color)
    {
        GameObject tempObject = new GameObject(name);
        tempObject.AddComponent<LineRenderer>();
        LineRenderer line = tempObject.GetComponent<LineRenderer>();
        line.startWidth = width;
        line.endWidth = width;
        line.useWorldSpace = true;
        line.GetComponent<Renderer>().material.color = color;
        line.startColor = color;
        line.endColor = color;
        return line;
    }

    public void Tick()
    {
        // Return if not enabled
        if (!enabled) return;

        Vector3 position = ball.GetPosition();

        velocityLine.SetPosition(0, position);
        velocityLine.SetPosition(1, position + ball.GetVelocity());

        windLine.SetPosition(0, position);
        windLine.SetPosition(1, position + wind.GetWindVector(ball.GetHeight())*SCALE);

        cupEffectLine.SetPosition(0, position);
        cupEffectLine.SetPosition(1, position + ball.GetCupEffect()*SCALE);

        fnetLine.SetPosition(0, position);
        fnetLine.SetPosition(1, position + ball.GetFnet()*SCALE);
    }

    public void Toggle() { enabled = !enabled; }
}
