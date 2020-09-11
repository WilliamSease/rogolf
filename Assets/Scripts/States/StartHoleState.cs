using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartHoleState : State
{
    public StartHoleState(Game game) : base(game) { }

    public override void Tick()
    {
        // Get next hole
        string nextHole = game.GetHoleBag().GetHole();

        // Save persistent game data
        game.Exit();

        // Load scene
        SceneManager.LoadScene(nextHole);

        // Load persistent game data
        GameObject godObject = GodObject.Create();
        godObject.AddComponent<Game>();
        game = godObject.GetComponent<Game>();

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
