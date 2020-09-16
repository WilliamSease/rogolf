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
    /// </summary>
    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            game.state.OnKeySpace();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            game.state.OnKeyUpArrow();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            game.state.OnKeyDownArrow();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            game.state.OnKeyRightArrow();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            game.state.OnKeyLeftArrow();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            game.state.OnKeyW();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            game.state.OnKeyA();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            game.state.OnKeyS();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            game.state.OnKeyD();
        }
        if (Input.GetKeyDown(KeyCode.Return)) // Enter key
        {
            game.state.OnKeyReturn();
        }
    }
}
