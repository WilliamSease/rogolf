using UnityEngine;

public class Game : MonoBehaviour
{
    // GameObject objects
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
        this.state = new StartGameState(this);
        this.inputController = new InputController(this);

        this.bag = new Bag(this);
        this.powerbar = new Powerbar(this);
    }

    /// <summary>
    /// Game Update function.
    /// Propogate Update to all relevant objects.
    /// </summary>
    public void Update()
    {
        //UnityEngine.Debug.Log(ball.GetComponent<Rigidbody>().velocity);
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

    public void SetHoleBag(HoleBag holeBag) { this.holeBag = holeBag; }
    public void SetItemBag(ItemBag itemBag) { this.itemBag = itemBag; }
}
