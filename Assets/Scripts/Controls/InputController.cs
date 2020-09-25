using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    private Game game;
	public Canvas console;

    private const float DELAY = 0.2f;
    private List<Tuple<KeyCode, Action>> keyboard;
    private float[] pressTimes;

    public InputController(Game game)
    {
        this.game = game;
		console = (GameObject.Find("DevConsole")).GetComponent<Canvas>();

        keyboard = new List<Tuple<KeyCode, Action>>();
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.Space, delegate() { game.state.OnKeySpace(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.UpArrow, delegate() { game.state.OnKeyUpArrow(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.DownArrow, delegate() { game.state.OnKeyDownArrow(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.RightArrow, delegate() { game.state.OnKeyRightArrow(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.LeftArrow, delegate() { game.state.OnKeyLeftArrow(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.W, delegate() { game.state.OnKeyW(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.A, delegate() { game.state.OnKeyA(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.S, delegate() { game.state.OnKeyS(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.D, delegate() { game.state.OnKeyD(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.Q, delegate() { game.state.OnKeyQ(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.E, delegate() { game.state.OnKeyE(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.Return, delegate() { game.state.OnKeyReturn(); }));

        pressTimes = new float[keyboard.Count];
    }

    /// <summary>
    /// Check each relevant key.
    /// If it is being pressed, propagate key press as method call to game's state.
    /// </summary>
    public void Tick()
    {
        // Ignore keystrokes if console is active
		if (console.enabled) return;

        // Else process keys
        float deltaTime = Time.deltaTime;
        for (int i = 0; i < keyboard.Count; i++) ProcessKey(i, deltaTime);
    }

    private void ProcessKey(int i, float deltaTime)
    {
        KeyCode keyCode = keyboard[i].Item1;
        if (Input.GetKey(keyCode))
        {
            UnityEngine.Debug.Log(keyCode);
            pressTimes[i] -= deltaTime;
            if (pressTimes[i] <= 0f)
            {
                Action onKeyPress = keyboard[i].Item2;
                onKeyPress();
                pressTimes[i] += DELAY;
            }
        }
        if (Input.GetKeyUp(keyCode))
        {
            pressTimes[i] = 0f;
        }
    }
}
