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
        Transform[] allChildren;
        try
        {
            allChildren = terrain.GetComponentsInChildren<Transform>();
        }
        catch (NullReferenceException e)
        {
            throw new InvalidOperationException("Terrain not found on scene " + holeName + ". Is the .blend file missing in the project?");
        }

        // Create props lists to process after ground colliders have been created
        List<GameObject> pinList = new List<GameObject>();
        List<GameObject> teeList = new List<GameObject>();
        List<GameObject> treeList = new List<GameObject>();

        foreach (Transform child in allChildren)
        {
            GameObject childObject = child.gameObject;
            string childName = childObject.name;
            // Skip iteration if component is the parent
            if (childName == holeName) continue;
            // Else if prop, add to approprate prop list
            else if (childName.StartsWith("Pin"))
            {
                pinList.Add(childObject);
                continue;
            }
            else if (childName.StartsWith("Tee"))
            {
                teeList.Add(childObject);
                continue;
            }
            else if (childName.StartsWith("Tree"))
            {
                treeList.Add(childObject);
                continue;
            }
            // Else a ground mesh
            else
            {
                // Add mesh collier
                childObject.AddComponent<MeshCollider>();

                // Modify materials of ground
                Renderer renderer = childObject.GetComponent<Renderer>();
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

        // Add props from prop list
        foreach (GameObject pin in pinList)
        {
            AddProp(pin);
        }
        foreach (GameObject tee in teeList)
        {
            AddProp(tee);
        }
        foreach (GameObject tree in treeList)
        {
            AddProp(tree);
        }

        // Reset per-hole data
        //game.ResetStrokes();
    }

    public void AddProp(GameObject gameObject)
    {
        try
        {
            RaycastHit hit = RaycastVertical(gameObject);
            gameObject.transform.position = hit.point;
            Destroy(gameObject);
        }
        catch (InvalidOperationException e)
        {
            UnityEngine.Debug.Log(e);
        }
    }

    public RaycastHit RaycastVertical(GameObject gameObject)
    {
        RaycastHit hit;
        // Check down
        if (Physics.Raycast(new Ray(gameObject.transform.position, Vector3.down), out hit))
        {
            return hit;
        }
        // Check up
        else if (Physics.Raycast(new Ray(gameObject.transform.position, Vector3.up), out hit))
        {
            return hit;
        }
        // Exception if not found
        else
        {
            throw new InvalidOperationException("RaycastHit not found for " + gameObject.name);
        }
    }
}
