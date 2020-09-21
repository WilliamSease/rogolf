using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor
{
    private Game game;

    private Vector3 position;

    public Cursor(Game game) {
        this.game = game;
    }

    public void Tick()
    {
        Vector3 p = game.GetBall().GetPosition();
        float distance = game.GetBag().GetClub().GetDistance();
        float angle = game.GetBall().GetAngle();
        p.x += distance * Mathf.Cos(angle);
        p.z += distance * Mathf.Sin(angle);
        position = p;

        // Set graphics
        game.GetGameController().TickCursor();
    }

    public Vector3 GetPosition() { return new Vector3(position.x, position.y, position.z); }
}
