using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CursorGraphics
{
    private const float CURSOR_HEIGHT = 1f;
    private const float CURSOR_SEGMENT_HEIGHT = 1.5f;

    private Game game;
    private Cursor cursor;
    private MouseOrbitImproved orbitalControls;
    private List<GameObject> cursorList;
    private GameObject cursorTextObject;

    public CursorGraphics(Game game)
    {
        this.game = game;
        cursor = game.GetCursor();
        orbitalControls = game.orbitalControls;
        cursorList = game.GetCursorList();
        cursorTextObject = game.GetCursorTextObject();
    }

    public void Enable()
    {
        foreach (GameObject cursor in game.GetCursorList()) { cursor.SetActive(true); }
        cursorTextObject.SetActive(true);
    }

    public void Disable()
    {
        foreach (GameObject cursor in game.GetCursorList()) { cursor.SetActive(false); }
        cursorTextObject.SetActive(false);
    }

    public void Tick()
    {
        // Update cursor GameObject
        Vector3 cursorPosition = cursor.GetPosition();
        cursorPosition.y += CURSOR_HEIGHT;
        for (int i = 0; i < cursorList.Count; i++)
        {
            Vector3 tempPos = new Vector3(cursorPosition.x, cursorPosition.y + (i * CURSOR_SEGMENT_HEIGHT), cursorPosition.z);
            cursorList[i].transform.localPosition = tempPos;
        }

        // Update cursor text
        cursorTextObject.GetComponent<TextMeshPro>().text = MathUtil.ToYardsRounded(game.GetBag().GetClub().GetDistance()) + "y";
        cursorTextObject.transform.localPosition = new Vector3(cursorPosition.x, cursorPosition.y + (4 * CURSOR_SEGMENT_HEIGHT), cursorPosition.z);
        cursorTextObject.transform.LookAt(game.GetCameraObject().transform);
    }
}
