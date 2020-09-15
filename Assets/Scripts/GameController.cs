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

    public Material green;
    public Material fairway;
    public Material rough;
    public Material bunker;
    public Material water;

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

    /// <summary>
    /// Called from the main menu.
    /// Resets old game data, and initializes new data.
    /// Starts the game loop by loading the first hole.
    /// </summary>
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

    /// <summary>
    /// Loads the scene asynchronously, then preps the new scene by calling NextHole()
    /// </summary>
    /// <param name="targetScene">Name of the new scene to load</param>
    /// <returns></returns>
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

        string holeName = SceneManager.GetActiveScene().name;

        // Modify blender scene
        GameObject terrain = GameObject.Find(holeName);
        Transform[] allChildren = terrain.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            string childName = child.gameObject.name;
            // Skip iteration if component is the parent
            if (childName == holeName) continue;
            // TODO - load correct prefabs here
            else if (childName.StartsWith("Pin")) continue;
            else if (childName.StartsWith("Tee")) continue;
            else if (childName.StartsWith("Tree")) continue;
            else
            {
                // Modify materials of ground
                Renderer renderer = child.gameObject.GetComponent<Renderer>();
                char type = childName[0];
                switch (type)
                {
                    case 'B':
                        renderer.material = bunker;
                        break;
                    case 'F':
                        renderer.material = fairway;
                        break;
                    case 'G':
                        renderer.material = green;
                        break;
                    case 'R':
                        renderer.material = rough;
                        break;
                    case 'W':
                        renderer.material = water;
                        break;
                    default:
                        UnityEngine.Debug.Log("Candidate material not found for mesh " + childName);
                        break;
                }
            }
        }

        // Reset per-hole data
        //game.ResetStrokes();
    }
}
