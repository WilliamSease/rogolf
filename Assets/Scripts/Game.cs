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
using TMPro;

namespace TargetEnum
{
    public enum Target { BALL, CURSOR, FREE }
}

public class Game : MonoBehaviour
{
    public GameController gc;

    // GameObject objects
    private GameObject cameraObject;
    private GameObject ballObject;
    private List<GameObject> cursorList;
    public GameObject freeFocus;
    public GameObject cursorTextObject;

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

    // Graphical-related helper classes
    private BallGraphics ballGraphics;
    private CursorGraphics cursorGraphics;
    private GraphicDebug graphicDebug;

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

    public void Begin()
    {
        // Initialize graphics helpers
        ballGraphics = new BallGraphics(this);
        cursorGraphics = new CursorGraphics(this);
        graphicDebug = new GraphicDebug(this);
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

        // Update camera target position
        if (target == Target.BALL) orbitalControls.targetPosition = ballPosition;
        if (target == Target.CURSOR) orbitalControls.targetPosition = cursorPosition;
        if (target == Target.FREE) 
		{
			orbitalControls.targetPosition = freeFocus.transform.position;
			/*This code pans. Or, it should. Maybe it shouldn't be here. Feel free to move. Maybe a little cumbersome?*/
			if(Input.GetMouseButton(0))
			{
				GetFreeFocus().transform.LookAt(GetCameraObject().transform);
				GetFreeFocus().transform.Translate(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
				RaycastHit hit;
				Vector3 positionHigh = new Vector3(GetFreeFocus().transform.position.x, GetFreeFocus().transform.position.x + 1000, GetFreeFocus().transform.position.z);
				Vector3 temp = GetFreeFocus().transform.position;
				if (Physics.Raycast(new Ray(positionHigh, Vector3.down), out hit))
				{
					temp.y = hit.point.y;
					GetFreeFocus().transform.position = temp;
				}
			}
		}
        if (target == Target.BALL) orbitalControls.targetPosition = ball.GetPosition();
        if (target == Target.CURSOR) orbitalControls.targetPosition = cursor.GetPosition();
        if (target == Target.FREE) orbitalControls.targetPosition = freeFocus.transform.position;

        ballGraphics.Tick();
        cursorGraphics.Tick();
        graphicDebug.Tick();
    }

    public void SetState(State state)
    {
        this.state.OnStateExit();
        //UnityEngine.Debug.Log(state);
        this.state = state;
        this.state.OnStateEnter();
    }

    public void ToggleTarget()
    {
        if (target == Target.BALL) target = Target.CURSOR;
        else if (target == Target.CURSOR) target = Target.FREE;
        else target = Target.BALL;
    }

    public void ToggleGraphicDebug() { graphicDebug.Toggle(); }

    public GameController GetGameController() { return gc; }

    public GameObject GetCameraObject() { return cameraObject; }
    public GameObject GetBallObject() { return ballObject; }
    public List<GameObject> GetCursorList() { return cursorList; }
    public GameObject GetCursorTextObject() { return cursorTextObject; }
    public void SetCameraObject(GameObject cameraObject) { this.cameraObject = cameraObject; }
    public void SetBallObject(GameObject ballObject) { this.ballObject = ballObject; }
    public void SetCursorList(List<GameObject> cursorList) { this.cursorList = cursorList; }
    public void SetCursorTextObject(GameObject to) { this.cursorTextObject = to; }

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
    public GameObject GetFreeFocus() { return freeFocus; }
    public Cursor GetCursor() { return cursor; }
    public CurrentDistance GetCurrentDistance() { return currentDistance; }
    public Bag GetBag() { return bag; }
    public Powerbar GetPowerbar() { return powerbar; }

    public CursorGraphics GetCursorGraphics() { return cursorGraphics; }
}
