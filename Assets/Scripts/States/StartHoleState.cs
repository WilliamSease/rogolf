using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartHoleState : State
{
    public StartHoleState(Game game) : base(game) { }

    public override void Tick()
    {
        // Load scene
        SceneManager.LoadScene(game.holeBag.GetHole());

        // Modify scene
        //  Add materials to ground

        // Load prefabs
        //  Add lighting
        //  Add ball
        //  Add camera?
        //  Add tees, hole, flag, etc

        // Reset per-hole data
        game.ResetStrokes();

        // Advance state
        game.SetState(new PrepareState(game));
    }
}
