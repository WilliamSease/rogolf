using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameController
{
    public static void StartGame(MainMenu mainMenu)
    {
        GameDataManager.ResetGameData();

        GameObject godObject = GodObject.Create();
        godObject.AddComponent<Game>();
        Game game = godObject.GetComponent<Game>();

        // Initialize game data
        game.SetHoleBag(new HoleBag());
        game.SetItemBag(new ItemBag());

        // Get next hole
        string nextHole = game.GetHoleBag().GetHole();
        
        // Save and destroy
        GameDataManager.SaveGameData(game);
        UnityEngine.Object.Destroy(godObject);

        // Load scene
        SceneManager.LoadScene(nextHole);
        mainMenu.NextHole(nextHole);
    }

    public static void NextHole()
    {
        UnityEngine.Debug.Log(SceneManager.GetActiveScene().name); // TODO - debug

        // Load persistent game data
        GameObject godObject = GodObject.Create();
        godObject.AddComponent<Game>();
        Game game = godObject.GetComponent<Game>();
        game.Init();

        // Modify scene
        //  Add materials to ground

        // Load prefabs
        //  Add lighting
        //  Add ball
        //  Add camera?
        //  Add tees, hole, flag, etc

        // Reset per-hole data
        //game.ResetStrokes();
    }
}
