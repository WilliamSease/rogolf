using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public const string NAME = "GameController";

    public GameObject camera;
    public GameObject ball;

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

    public void NextHole()
    {
        // Load persistent game data
        GameObject godObject = GodObject.Create();
        godObject.AddComponent<Game>();
        Game game = godObject.GetComponent<Game>();
        game.Init();

        /* Modify Scene */
        //  Add lighting
        // TODO - we're just using stock lighting for now

        //  Add ball
        Instantiate(ball);

        // Add camera and controls
        Instantiate(camera);
        MouseOrbitImproved orbitalControls = camera.GetComponent<MouseOrbitImproved>();
        orbitalControls.target = ball.transform;

        // Get children of blender scene
        GameObject terrain = GameObject.Find(SceneManager.GetActiveScene().name);
        Transform[] allChildren = terrain.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            // TODO - modify materials of ground
            // TODO - swap in prefabs for tees, hole, trees
            UnityEngine.Debug.Log(child.gameObject);
        }

        // Reset per-hole data
        //game.ResetStrokes();
    }
}
