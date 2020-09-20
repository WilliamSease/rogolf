using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private const float BALL_HEIGHT = 0.1f;

    // GameObject objects
    public GameObject godObject;
    public GameObject ballObject;

    public MouseOrbitImproved orbitalControls;

    // Other game objects (that aren't game objects)
    public State state;
    public InputController inputController;

    // Persistent game objects
    public HoleBag holeBag;
    public ItemBag itemBag;
    public PlayerAttributes playerAttributes;
    public TerrainAttributes terrainAttributes;

    // GAME OBJECT (not GameObject)
    public HoleInfo holeInfo;
    public Ball ball;
    public Bag bag;
    public Powerbar powerbar;

    // Game parameters
    private int strokes;

    public void Init()
    {
        this.state = new NoState(this);
        LoadGameData();
    }

    /// <summary>
    /// Game Start function.
    /// Just call Init().
    /// </summary>
    void Start() { }

    /// <summary>
    /// Game Update function.
    /// Propogate Update to all relevant objects.
    /// </summary>
    void Update()
    {
        inputController.Tick();
        state.Tick();

        // Update ball GameObject and controls
        Vector3 ballPosition = ball.GetPosition();
        ballPosition.y += BALL_HEIGHT;
        ballObject.transform.localPosition = ballPosition;
        orbitalControls.targetPosition = ballPosition;

        // Send Game reference to other objects
        GodOfUI ui = (GodOfUI)GameObject.Find("UICanvas").GetComponent<GodOfUI>();
        ui.gameRef = this;
    }

    /// <summary>
    /// Some state will call this method when the hole is over.
    /// It needs to...
    ///     1. Save any relevant data using binary files
    ///     2. Destroy anything we've instantiated
    ///     3. Move on to the next scene (and add a new GodObject to it???)
    /// </summary>
    public void Exit()
    {
        SaveGameData();
        Destroy(this);
    }

    /// <summary>
    /// Save game data to binary file
    /// </summary>
    public void SaveGameData()
    {
        GameDataManager.SaveGameData(this);
    }

    /// <summary>
    /// Load game data from binary file
    /// </summary>
    public void LoadGameData()
    {
        GameData gameData = GameDataManager.LoadGameData();
        this.holeBag = gameData.holeBag;
        this.itemBag = gameData.itemBag;
        this.playerAttributes = gameData.playerAttributes;
        this.terrainAttributes = gameData.terrainAttributes;

        Initialize();
    }

    public void CreateGameData()
    {
        this.state = new NoState(this);

        this.holeBag = new HoleBag();
        this.itemBag = new ItemBag();
        this.playerAttributes = new PlayerAttributes();
        this.terrainAttributes = new TerrainAttributes();

        Initialize();
    }

    private void Initialize()
    {
        inputController = new InputController(this);

        ball = new Ball(this);
        bag = new Bag(this);
        powerbar = new Powerbar(this);
    }

    public void SetState(State state)
    {
        this.state.OnStateExit();
        UnityEngine.Debug.Log(state);
        this.state = state;
        this.state.OnStateEnter();
    }

    public GameController GetGameController()
    {
        return (GameController) GameObject.Find(GameController.NAME).GetComponent<GameController>();
    }

    public int GetStrokes() { return strokes; }
    public void ResetStrokes() { strokes = 0; }
    public void IncrementStrokes() { ++strokes; }

    public HoleBag GetHoleBag() { return holeBag; }
    public ItemBag GetItemBag() { return itemBag; }
    public PlayerAttributes GetPlayerAttributes() { return playerAttributes; }
    public TerrainAttributes GetTerrainAttributes() { return terrainAttributes; }
    public void SetHoleBag(HoleBag holeBag) { this.holeBag = holeBag; }
    public void SetItemBag(ItemBag itemBag) { this.itemBag = itemBag; }
    public void SetPlayerAttributes(PlayerAttributes playerAttributes) { this.playerAttributes = playerAttributes; }
    public void SetTerrainAttributes(TerrainAttributes terrainAttributes) { this.terrainAttributes = terrainAttributes; }
    public void SetHoleInfo(HoleInfo holeInfo) { this.holeInfo = holeInfo; }

    public HoleInfo GetHoleInfo() { return holeInfo; }
    public Ball GetBall() { return ball; }
    public Bag GetBag() { return bag; }
    public Powerbar GetPowerbar() { return powerbar; }
}
