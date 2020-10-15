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
        SetHeight();

        // Set graphics
        game.GetGameController().TickCursor();
    }

    private void SetHeight()
    {
        RaycastHit hit;
        Vector3 positionHigh = new Vector3(position.x, position.y + 1000, position.z);
        if (Physics.Raycast(new Ray(positionHigh, Vector3.down), out hit))
        {
            position.y = hit.point.y;
        }
        // Else don't do anything -- cursor is already at ball height
    }

    public float GetRelativeHeight() { return position.y - game.GetBall().GetPosition().y; }

    public Vector3 GetPosition() { return new Vector3(position.x, position.y, position.z); }
}
