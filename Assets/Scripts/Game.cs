using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using TargetEnum;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TargetEnum
{
    public enum Target { BALL, CURSOR, FREE }
}

public class Game : MonoBehaviour
{
    private const float BALL_HEIGHT = 0.1f;
    private const float CURSOR_HEIGHT = 1f;
    private const float CURSOR_SEGMENT_HEIGHT = 1.5f;

    public GameController gc;

    // GameObject objects
    private GameObject cameraObject;
    private GameObject ballObject;
    private List<GameObject> cursorList;
    public GameObject freeFocus;

    public Target target;
    public MouseOrbitImproved orbitalControls;

    // Other game objects (that aren't game objects)
    private State state;
    private InputController inputController;

    // Game data objects
    private HoleBag holeBag;
    private ItemBag itemBag;
    private PlayerAttributes playerAttributes;
    private TerrainAttributes terrainAttributes;

    // GAME OBJECT (not GameObject)
    private HoleInfo holeInfo;
    private Wind wind;
    private Ball ball;
    private Cursor cursor;
    private CurrentDistance currentDistance;
    private Bag bag;
    private Powerbar powerbar;

    /// <summary>
    /// Performs initialization of Game object.
    /// </summary>
    public void Initialize()
    {
        this.state = new NoState(this);
        gc = GameObject.Find(GameController.NAME).GetComponent<GameController>();
        
        // Initialize fields
        this.holeBag = new HoleBag();
        this.itemBag = new ItemBag();
        this.playerAttributes = new PlayerAttributes();
        this.terrainAttributes = new TerrainAttributes();

        inputController = new InputController(this);
        target = Target.BALL;

        wind = new Wind(this);
        ball = new Ball(this);
        cursor = new Cursor(this);
        currentDistance = new CurrentDistance(this);
        bag = new Bag(this);
        powerbar = new Powerbar(this);

        // Send Game reference to other objects
        GodOfUI ui = GameObject.Find(GodOfUI.NAME).GetComponent<GodOfUI>();
        ui.gameRef = this;
    }

    /// <summary>
    /// Game Start function.
    /// Just call Initialize().
    /// </summary>
    void Start() { }

    /// <summary>
    /// Game Update function.
    /// Propagate Update to all relevant objects.
    /// </summary>
    void Update()
    {
        inputController.Tick();
        state.Tick();

        // Update ball GameObject
        Vector3 ballPosition = ball.GetPosition();
        ballPosition.y += BALL_HEIGHT;
        ballObject.transform.localPosition = ballPosition;

        // Update cursor GameObject
        Vector3 cursorPosition = cursor.GetPosition();
        cursorPosition.y += CURSOR_HEIGHT;
        for (int i = 0; i < cursorList.Count; i++)
        {
            Vector3 tempPos = new Vector3(cursorPosition.x, cursorPosition.y + (i * CURSOR_SEGMENT_HEIGHT), cursorPosition.z);
            cursorList[i].transform.localPosition = tempPos;
        }

        // Update camera target position
        if (target == Target.BALL) orbitalControls.targetPosition = ballPosition;
        if (target == Target.CURSOR) orbitalControls.targetPosition = cursorPosition;
        if (target == Target.FREE) orbitalControls.targetPosition = freeFocus.transform.position;
    }

    public void SetState(State state)
    {
        this.state.OnStateExit();
        UnityEngine.Debug.Log(state);
        this.state = state;
        this.state.OnStateEnter();
    }

    public void ToggleTarget() {
        if (target == Target.BALL) target = Target.CURSOR;
        else if (target == Target.CURSOR) target = Target.FREE;
        else target = Target.BALL;
    }

    public GameController GetGameController() { return gc; }

    public GameObject GetCameraObject() { return cameraObject; }
    public GameObject GetBallObject() { return ballObject; }
    public List<GameObject> GetCursorList() { return cursorList; }
    public void SetCameraObject(GameObject cameraObject) { this.cameraObject = cameraObject; }
    public void SetBallObject(GameObject ballObject) { this.ballObject = ballObject; }
    public void SetCursorList(List<GameObject> cursorList) { this.cursorList = cursorList; }

    public State GetState() { return state; }
    
    public HoleBag GetHoleBag() { return holeBag; }
    public ItemBag GetItemBag() { return itemBag; }
    public PlayerAttributes GetPlayerAttributes() { return playerAttributes; }
    public TerrainAttributes GetTerrainAttributes() { return terrainAttributes; }
    public void SetHoleBag(HoleBag holeBag) { this.holeBag = holeBag; }
    public void SetItemBag(ItemBag itemBag) { this.itemBag = itemBag; }
    public void SetPlayerAttributes(PlayerAttributes playerAttributes) { this.playerAttributes = playerAttributes; }
    public void SetTerrainAttributes(TerrainAttributes terrainAttributes) { this.terrainAttributes = terrainAttributes; }
    public void SetHoleInfo(HoleInfo holeInfo) { this.holeInfo = holeInfo; }

    public Target GetTarget() { return target; }
    public HoleInfo GetHoleInfo() { return holeInfo; }
    public Wind GetWind() { return wind; }
    public Ball GetBall() { return ball; }
    public GameObject getFreeFocus() { return freeFocus; }
    public Cursor GetCursor() { return cursor; }
    public CurrentDistance GetCurrentDistance() { return currentDistance; }
    public Bag GetBag() { return bag; }
    public Powerbar GetPowerbar() { return powerbar; }
}
