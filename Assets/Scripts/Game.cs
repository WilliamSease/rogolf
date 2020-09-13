using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    // GameObject objects
    public GameObject godObject;
    public GameObject ball;

    // Other game objects (that aren't game objects)
    public State state;
    public InputController inputController;

    // Persistent game objects
    public HoleBag holeBag;
    public ItemBag itemBag;

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
        // If save file is not found
        if (gameData == null)
        {
            GameDataManager.ResetGameData();
        }
        else
        {
            this.holeBag = gameData.holeBag;
            this.itemBag = gameData.itemBag;
        }
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

    public HoleBag GetHoleBag() { return holeBag; }
    public ItemBag GetItemBag() { return itemBag; }
    public void SetHoleBag(HoleBag holeBag) { this.holeBag = holeBag; }
    public void SetItemBag(ItemBag itemBag) { this.itemBag = itemBag; }
}
