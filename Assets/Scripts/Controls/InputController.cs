using UnityEngine;

public class InputController
{
    private Game game;

    public InputController(Game game)
    {
        this.game = game;
    }

    /// <summary>
    /// Check each relevant key.
    /// If it is being pressed, propogate key press as method call to game's state.
    /// 
    /// This is hard coded right now! Deal with it!
    /// 
    /// We will likely need to buffer input for many of the keys.
    /// </summary>
    public void Tick()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //UnityEngine.Debug.Log("Space");
            game.state.OnKeySpace();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //UnityEngine.Debug.Log("UpArrow");
            game.state.OnKeyUpArrow();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //UnityEngine.Debug.Log("DownArrow");
            game.state.OnKeyDownArrow();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //UnityEngine.Debug.Log("RightArrow");
            game.state.OnKeyRightArrow();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //UnityEngine.Debug.Log("LeftArrow");
            game.state.OnKeyLeftArrow();
        }
        if (Input.GetKey(KeyCode.Return)) // Enter key
        {
            //UnityEngine.Debug.Log("Return");
            game.state.OnKeyReturn();
        }
    }
}
