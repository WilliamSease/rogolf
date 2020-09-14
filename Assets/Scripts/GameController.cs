using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public const string NAME = "GameController";

    void Start()
    {
        // We need to control the game for the whole game! Don't we?!?
        DontDestroyOnLoad(this);
    }

    void Update() { }

    public static GameController GetInstance()
    {
        GameObject gameObject = GameObject.Find(NAME);
        if (gameObject != null)
        {
            return gameObject.GetComponent<GameController>();
        }
        else
        {
            throw new InvalidOperationException("GameController GameObject not found.");
        }
    }

    public void StartGame()
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
        LoadScene(nextHole);
    }

    public void LoadScene(string nextHole)
    {
        StartCoroutine(AsyncSceneLoad(nextHole));
    }

    IEnumerator AsyncSceneLoad(string targetScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        NextHole();
    }

    public static void NextHole()
    {
        // Load persistent game data
        GameObject godObject = GodObject.Create();
        godObject.AddComponent<Game>();
        Game game = godObject.GetComponent<Game>();
        game.Init();

        UnityEngine.Debug.Log(game.state);

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
