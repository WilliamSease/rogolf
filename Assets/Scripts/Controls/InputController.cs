using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    private Game game;
	public Canvas console;

    private const float SLOW_DELAY = 0.5f;
    private const float FAST_DELAY = 0.05f;
    private List<Tuple<KeyCode, Action>> keyboard;
    private float[] pressTimes;

    public InputController(Game game)
    {
        this.game = game;
		console = (GameObject.Find(DevConsole.NAME)).GetComponent<Canvas>();

        keyboard = new List<Tuple<KeyCode, Action>>();
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.Space, delegate() { game.GetState().OnKeySpace(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.UpArrow, delegate() { game.GetState().OnKeyUpArrow(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.DownArrow, delegate() { game.GetState().OnKeyDownArrow(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.RightArrow, delegate() { game.GetState().OnKeyRightArrow(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.LeftArrow, delegate() { game.GetState().OnKeyLeftArrow(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.W, delegate() { game.GetState().OnKeyW(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.A, delegate() { game.GetState().OnKeyA(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.S, delegate() { game.GetState().OnKeyS(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.D, delegate() { game.GetState().OnKeyD(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.Q, delegate() { game.GetState().OnKeyQ(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.E, delegate() { game.GetState().OnKeyE(); }));
        keyboard.Add(new Tuple<KeyCode, Action>(KeyCode.Return, delegate() { game.GetState().OnKeyReturn(); }));

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
        if (Input.GetKeyDown(keyCode))
        {
            Action onKeyPress = keyboard[i].Item2;
            onKeyPress();
            pressTimes[i] = SLOW_DELAY;
        }
        else if (Input.GetKey(keyCode))
        {
            pressTimes[i] -= deltaTime;
            if (pressTimes[i] <= 0f)
            {
                Action onKeyPress = keyboard[i].Item2;
                onKeyPress();
                pressTimes[i] += FAST_DELAY;
            }
        }
    }
}
