using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public HoleBag holeBag;
    public ItemBag itemBag;
}

public class Game : MonoBehaviour
{
    private const string PLAYER_PREFS_KEY = "GameData";

    // GameObject objects
    public GameObject godObject;
    public GameObject ball;

    // Other game objects (that aren't game objects)
    public State state;
    public InputController inputController;

    // Persistent game objects
    public GameData gameData;

    // GAME OBJECT (not GameObject)
    public Bag bag;
    public Powerbar powerbar;

    // Game parameters
    private int strokes;

    /// <summary>
    /// Game Start function.
    /// Initialize state and Instantiate anything we need,
    /// </summary>
    public void Start()
    {
        LoadGameData();
    }

    /// <summary>
    /// Game Update function.
    /// Propogate Update to all relevant objects.
    /// </summary>
    public void Update()
    {
        //UnityEngine.Debug.Log(SceneManager.GetActiveScene().name);
        UnityEngine.Debug.Log(state);
        inputController.Tick();
        state.Tick();
    }

    /// <summary>
    /// Some state will call this method when the hole is over.
    /// It needs to...
    ///     1. Save any relevant data using PlayerPrefs
    ///     2. Destroy anything we've instantiated
    ///     3. Move on to the next scene (and add a new GodObject to it???)
    /// </summary>
    public void Exit()
    {
        SaveGameData();
        Destroy(this);
    }

    /// <summary>
    /// Load game data from PlayerPrefs
    /// </summary>
    public void LoadGameData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        string text = PlayerPrefs.GetString(PLAYER_PREFS_KEY);
        // If gameData is empty, instantiate the game
        if (string.IsNullOrEmpty(text))
        {
            gameData = new GameData();
            state = new StartGameState(this);
            Initialize();
        }
        // Else instantiate the next hole
        else
        {
            using (var reader = new System.IO.StringReader(text))
            {
                gameData = serializer.Deserialize(reader) as GameData;
            }
            state = new StartHoleState(this);
            Initialize();
        }
    }

    /// <summary>
    /// Save game data to PlayerPrefs
    /// </summary>
    public void SaveGameData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        using (StringWriter sw = new StringWriter())
        {
            serializer.Serialize(sw, gameData);
            PlayerPrefs.SetString(PLAYER_PREFS_KEY, sw.ToString());
            //UnityEngine.Debug.Log(sw.ToString());
        }
    }

    /// <summary>
    /// Reset PlayerPrefs game data
    /// </summary>
    public static void ResetGameData()
    {
        PlayerPrefs.SetString(PLAYER_PREFS_KEY, "");
    }

    private void Initialize()
    {
        inputController = new InputController(this);

        bag = new Bag(this);
        powerbar = new Powerbar(this);
    }

    public void SetState(State state)
    {
        this.state.OnStateExit();
        this.state = state;
        this.state.OnStateEnter();
    }

    public int GetStrokes() { return strokes; }
    public void ResetStrokes() { strokes = 0; }
    public void IncrementStrokes() { ++strokes; }

    public HoleBag GetHoleBag() { return gameData.holeBag; }
    public ItemBag GetItemBag() { return gameData.itemBag; }
    public void SetHoleBag(HoleBag holeBag) { gameData.holeBag = holeBag; }
    public void SetItemBag(ItemBag itemBag) { gameData.itemBag = itemBag; }
}
