using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class DevConsole : MonoBehaviour
{
    public const string NAME = "DevConsole";

    public GameController gc;
    private Game game;
    
    public Canvas thisCanvas;
    public Text[] bodyText = new Text[20];
    public InputField inputField;
    public Text inputText;
    private int memory;
    
    private string[] splash = 
    {
        "Rogolf 2020 William Sease & Matthew Swanson",
        "Try \"Help\""
    };
    
    private string[] helpMessage =
    {   "Help {general|game|ball|physics|tools}"
    };

    private string[] helpGeneral =
    {   "Scene name: Attempt to load a scene (unstable)",
        "Status: Display status info."
    };

    private string[] helpGame =
    {   "GetPlayer: Get player attribute",
        "SetPlayer: Set player attribute",
        "PayPlayer: Give player some money",
        "GetWind: Get wind speed and angle.",
        "SetWind: Set wind speed and angle.",
        "GetHoleData: Gets info about the current hole.",
        "GiveItem: Gives the player item of specified name.",
        "NextHole: Sets the next hole.",
        "EndHole: End current hole."
    };

    private string[] helpBall =
    {   "MoveBall {Abs|Rel} x y z: Places the ball.",
        "GetBallPos: Prints the ball's position.",
        "GetRemaining: Get 3D distance to hole.",
        "ToTee [{front|back}]: Move the ball to tee.",
        "ToHole: Move the ball to hole."
    };

    private string[] helpPhysics =
    {   "GetBallPhysics: Get ball physics parameters.",
        "SetBallPhysics: Set ball physics parameters.",
        "GetTerrain: Get terrain attribute.",
        "SetTerrain: Set terrain attribute."
    };

    private string[] helpTools =
    {   "GenerateClubs: Send optimal club parameters to .csv.",
        "SimulatePower: Send club power data to .csv.",
        "GraphicDebug: Toggle graphic debug.",
        "Strike: Hit ball.",
		"PlaySound [name]: Play a sound.",
		"SetVolume [0.0 - 1.0]: Set game volume.",
        "WriteLeaderBoard: Write dummy values to leaderboard."
    };
    
    // Start is called before the first frame update
    void Start()
    {
        Clear();
        PumpArr(splash);
        thisCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
            thisCanvas.enabled = !thisCanvas.enabled;
        if(!thisCanvas.enabled)
        {
            inputField.DeactivateInputField();
            inputField.text = "";
            memory = 20;
            return;
        }
        inputField.text = inputField.text.Replace("`", string.Empty);
        inputField.Select();
        inputField.ActivateInputField();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Execute(inputText.text);
            inputField.text = "";
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            memory = (memory > 0) ? memory - 1 : 0;
            inputField.text = bodyText[memory].text;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            memory = (memory < 19) ? memory + 1 : 19;
            inputField.text = bodyText[memory].text;
        }
    }
    
    void Clear()
    {
        for(int i = 0; i < 20; i++)
            Pump("");
    }
    
    void Pump(string str)
    {
        for(int i = 0; i < 19; i++)
            bodyText[i].text = bodyText[i+1].text;
        bodyText[19].text = str;
    }
    
    void PumpArr(string[] arr)
    {
        for(int i = 0; i < arr.Length; i++)
            Pump(arr[i]);
    }
    
    void Reply(string str)
    {
        Pump("System: " + str);
    }
    
    void Report(bool succ)
    {
        if (!succ) Reply("Unspecified failure.");
    }
    
    void Execute(string str)
    {
        try { game = GameObject.Find(GameController.NAME).GetComponent<Game>(); }
        catch (NullReferenceException) { Reply("Game is currently null. Functionality is limited!"); }
        if(str.Length > 0) Pump(str);
        string[] arr = Regex.Split(str, " ");
        arr[0] = arr[0].ToLower();
        if (arr.Length == 0) return;
        switch(arr[0])
        {
            case "help":
                Help(Tail(arr));
                break;
            case "scene":
                Report(Scene(arr[1]));
                break;
            case "status":
                Report(Status());
                break;
            case "clear":
                Clear();
                break;
            case "absmov":
                Report(AbsMov(Tail(arr)));
                break;
            case "moveball":
                Report(MoveBall(arr[1].ToLower(), Floatify(arr[2]), Floatify(arr[3]), Floatify(arr[4])));
                break;
            case "getballpos":
                Report(GetBallPos());
                break;
            case "totee":
                Report(ToTee(Tail(arr)));
                break;
            case "setwind":
                Report(SetWind(Tail(arr)));
                break;
            case "getwind":
                Report(GetWind());
                break;
            case "generateclubs":
                Report(GenerateClubs());
                break;
            case "setterrain":
                Report(SetTerrain(Tail(arr)));
                break;
            case "getterrain":
                Report(GetTerrain(Tail(arr)));
                break;
            case "getballphysics":
                Report(GetBallPhysics(Tail(arr)));
                break;
            case "setballphysics":
                Report(SetBallPhysics(Tail(arr)));
                break;
            case "getremaining":
                Report(GetRemaining());
                break;
            case "tohole":
                Report(ToHole());
                break;
            case "nexthole":
                Report(NextHole(Tail(arr)));
                break;
            case "endhole":
                Report(EndHole());
                break;
            case "getholedata":
                Report(GetHoleData());
                break;
            case "getplayer":
                Report(GetPlayer(Tail(arr)));
                break;
            case "setplayer":
                Report(SetPlayer(Tail(arr)));
                break;
            case "payplayer":
                Report(PayPlayer(Tail(arr)));
                break;
			case "playsound":
				Report(PlaySound(Tail(arr)));
				break;
            case "graphicdebug":
                Report(GraphicDebug());
                break;
			case "setvolume":
				Report(SetVolume(Tail(arr)));
				break;
			case "itsover9000":
				Report(What9000());
				break;
            case "giveitem":
                Report(GiveItem(Tail(arr)));
                break;
            case "strike":
                Report(Strike(Tail(arr)));
                break;
            case "simulatepower":
                Report(SimulatePower());
                break;
            case "writeleaderboard":
                Report(WriteLeaderBoard());
                break;
            default:
                Reply("'" + arr[0] + "' doesn't appear to be a command");
                break;
        }
    }

    public bool Help(string[] arr)
    {
        string[] message;
        if (arr.Length == 1)
        {
            switch (arr[0].ToLower())
            {
                case "general":
                    message = helpGeneral;
                    break;
                case "ball":
                    message = helpBall;
                    break;
                case "physics":
                    message = helpPhysics;
                    break;
                case "tools":
                    message = helpTools;
                    break;
				case "game":
					message = helpGame;
					break;
                default:
                    message = helpMessage;
                    break;
            }
        }
        else message = helpMessage;
        PumpArr(message);
        return true;
    }
    
    public bool Scene(string str)
    {
        Reply("Attempting Scene Load...");
        Reply("Out of Order"); //gc.LoadScene(str);
        return true;
    }
    
    public bool Status()
    {
        Pump("***STATUS***");
        Pump("Memory Usage: " + System.GC.GetTotalMemory(true) / 1000000 + "MB");
        Pump("Uptime: " + (int) Time.realtimeSinceStartup / 60 + "m " + ((int) Time.realtimeSinceStartup % 60) + "s");
        Pump("Rendering: " + SystemInfo.graphicsDeviceName);
		Pump("Instantaneous FPS: " + (1f / Time.deltaTime) + (((1f / Time.deltaTime) >= 60) ? " (Seems Good!)" : " (Yikes!)"));
        return true;
    }
    
    public bool MoveBall(string type, float x, float y, float z)
    {
        if (type.Equals("abs")) 
            game.GetBall().SetPosition(new Vector3(x,y,z));
        else if (type.Equals("rel"))
            game.GetBall().SetRelativePosition(x,y,z);
        else { Reply("MoveBall: You must specify 'abs' or 'rel'!"); return false; }
        game.GetBall().AngleToHole();
        return true;
    }
    
    public bool GetBallPos() 
    { 
        Vector3 v = game.GetBall().GetPosition();
        Reply("GetBallPos: " + v[0] + " " + v[1] + " " + v[2]);
        return true;
    }
    
    public bool AbsMov(string[] arr)
    {
        GameObject g = GameObject.Find(arr[0]);
        if (g == null) { Reply("GameObject " + arr[0] + " appears not to exist."); return false; }
        else g.transform.position = new Vector3(Floatify(arr[1]),Floatify(arr[2]),Floatify(arr[3]));
        return true;
    }

    public bool ToTee(string[] arr)
    {
        string errorMessage = "ToTee [{front|back}]";
        if (arr.Length == 0)
        {
            game.GetBall().SetPosition(game.GetHoleInfo().GetTeePosition());
        }
        else if (arr.Length == 1)
        {
            char c = arr[0].ToLower()[0];
            if (c == 'f') game.GetBall().SetPosition(game.GetHoleInfo().GetFrontTeePosition());
            else if (c == 'b') game.GetBall().SetPosition(game.GetHoleInfo().GetBackTeePosition());
            else {
                Reply(errorMessage);
                return true;
            }
        }
        else { 
            Reply(errorMessage);
            return true;
        }
        game.GetBall().AngleToHole();
        return true;
    }

    public bool SetWind(string[] arr)
    {
        string errorMessage = "SetWind {off|on|speed angle}";
        Wind wind = game.GetWind();
        if (arr.Length == 0) {
            Reply(errorMessage);
            return false;
        }
        else if (arr.Length == 1)
        {
            if (arr[0] == "off") { wind.Disable(); }
            else if (arr[0] == "on") { wind.Reset(); }
            else
            {
                Reply(errorMessage);
                return false;
            }
        }
        else if (arr.Length == 2)
        {
            wind.SetSpeed(Floatify(arr[0]));
            wind.SetAngle(Floatify(arr[1]));
        }
        else
        {
            Reply(errorMessage);
            return false;
        }
        return true;
    }

    public bool GetWind()
    {
        Wind wind = game.GetWind();
        Reply(String.Format("Speed: {0}, Angle: {1}", wind.GetSpeed(), wind.GetAngle()));
        return true;
    }

    public bool GenerateClubs()
    {
        game.GetBag().GenerateClubs();
        Reply("Done.");
        return true;
    }

    public bool SetTerrain(string[] arr)
    {
        string errorMessage1 = "SetTerrain {g|f|r|b|w}";
        string errorMessage2 = "    {friction|bounce|lieRate|lieRange} f";
        if (arr.Length == 3)
        {
            TerrainType terrainType = game.GetTerrainAttributes().GetTerrainType(arr[0].ToUpper());
            float n = Floatify(arr[2]);
            switch (arr[1].ToLower())
            {
                case "friction":
                    terrainType.SetFriction(n);
                    break;
                case "bounce":
                    terrainType.SetBounce(n);
                    break;
                case "lierate":
                    terrainType.SetLieRate(n);
                    break;
                case "lierange":
                    terrainType.SetLieRange(n);
                    break;
                default:
                    Reply(errorMessage1);
                    Reply(errorMessage2);
                    break;
            }
            return true;
        }
        Reply(errorMessage1);
        Reply(errorMessage2);
        return true;
    }

    public bool GetTerrain(string[] arr)
    {
        string errorMessage1 = "GetTerrain {g|f|r|b|w}";
        string errorMessage2 = "    {friction|bounce|lieRate|lieRange}";
        if (arr.Length == 2)
        {
            TerrainType terrainType = game.GetTerrainAttributes().GetTerrainType(arr[0].ToUpper());
            switch (arr[1].ToLower())
            {
                case "friction":
                    Reply(terrainType.GetFriction().ToString());
                    break;
                case "bounce":
                    Reply(terrainType.GetBounce().ToString());
                    break;
                case "lierate":
                    Reply(terrainType.GetLieRate().ToString());
                    break;
                case "lierange":
                    Reply(terrainType.GetLieRange().ToString());
                    break;
                default:
                    Reply(errorMessage1);
                    Reply(errorMessage2);
                    break;
            }
            return true;
        }
        Reply(errorMessage1);
        Reply(errorMessage2);
        return true;
    }
    
    public bool GetBallPhysics(string[] arr)
    {
        string errorMessage1 = "GetBallPhysics TODO";
        if (arr.Length == 1)
        {
            Ball ball = game.GetBall();
            switch (arr[0].ToLower())
            {
                case "mass":
                    Reply(ball.GetMass().ToString());
                    break;
                case "radius":
                    Reply(ball.GetRadius().ToString());
                    break;
                default:
                    Reply(errorMessage1);
                    break;
            }
            return true;
        }
        Reply(errorMessage1);
        return true;
    }

    public bool SetBallPhysics(string[] arr)
    {
        string errorMessage1 = "SetBallPhysics TODO";
        if (arr.Length == 2)
        {
            Ball ball = game.GetBall();
            float n = Floatify(arr[1]);
            switch (arr[0].ToLower())
            {
                case "mass":
                    ball.SetMass(n);
                    break;
                case "radius":
                    ball.SetRadius(n);
                    break;
                default:
                    Reply(errorMessage1);
                    break;
            }
            return true;
        }
        Reply(errorMessage1);
        return true;
    }

    public bool GetRemaining()
    {
        Reply(game.GetBall().DistanceToHole().ToString());
        return true;
    }

    public bool ToHole()
    {
        game.GetBall().SetPosition(game.GetHoleInfo().GetHolePosition());
        return true;
    }
    
    public bool NextHole(string[] name)
    {
        string errorMessage = "nexthole name";
        if (name.Length == 1)
        {
            GameObject.Find("GameController").GetComponent<Game>().GetHoleBag().SetQueueUp(name[0]);
            return true;
        }
        Reply(errorMessage);
        return true;
    }

    public bool EndHole()
    {
        game.SetState(new PostHoleState(game));
        return true;
    }

    public bool GetHoleData()
    {
        Reply(game.GetHoleBag().GetCurrentHoleData().ToString());
        return true;
    }

    public bool GetPlayer(string[] arr)
    {
        string errorMessage = "GetPlayer [{power|control|impact|spin}]";
        PlayerAttributes pa = game.GetPlayerAttributes();
        if (arr.Length == 0)
        {
            Reply(pa.ToString());
            return true;
        }
        else if (arr.Length == 1)
        {
            switch (arr[0])
            {
                case "power":
                    Reply(pa.GetPower().ToString());
                    break;
                case "control":
                    Reply(pa.GetControl().ToString());
                    break;
                case "impact":
                    Reply(pa.GetImpact().ToString());
                    break;
                case "spin":
                    Reply(pa.GetSpin().ToString());
                    break;
                default:
                    Reply(errorMessage);
                    break;
            }
            return true;
        }
        Reply(errorMessage);
        return true;
    }

    public bool SetPlayer(string[] arr)
    {
        string errorMessage = "SetPlayer {power|control|impact|spin} n";
        if (arr.Length == 2)
        {
            PlayerAttributes pa = game.GetPlayerAttributes();
            float n = Floatify(arr[1]);
            switch (arr[0])
            {
                case "power":
                    pa.SetPower(n);
                    break;
                case "control":
                    pa.SetControl(n);
                    break;
                case "impact":
                    pa.SetImpact(n);
                    break;
                case "spin":
                    pa.SetSpin(n);
                    break;
                default:
                    Reply(errorMessage);
                    break;
            }
            game.GetBag().UpdateDistances();
            return true;
        }
        Reply(errorMessage);
        return true;
    }
    
    public bool PayPlayer(string[] arr)
    {
        string errorMessage = "payplayer n";
        if (arr.Length == 1)
        {
            game.GetScore().AddCredit(Intify(arr[0]));
            return true;
        }
        Reply(errorMessage);
        return true;
    }
	
	public bool PlaySound(string[] arr)
	{
		string errorMessage = "PlaySound name";
		if(arr.Length == 1)
		{
			Reply("Clip: " + Enum.Parse(typeof(SoundEnum.Sound), arr[0].ToUpper().ToString()) + " Volume: " + BoomBox.GetVolumeStat());
			return BoomBox.Play((SoundEnum.Sound) Enum.Parse(typeof(SoundEnum.Sound), arr[0].ToUpper()));
		}
		Reply(errorMessage);
		return true;
	}
	
	public bool SetVolume(string[] arr)
	{
		string errorMessage = "SetVolume vol";
		if (arr.Length == 1)
		{
			BoomBox.SetVolumeStat(Floatify(arr[0]));
			return true;
		}
		Reply(errorMessage);
		return true;
	}

    public bool GraphicDebug()
    {
        game.ToggleGraphicDebug();
        return true;
    }
	
	public bool What9000()
	{
		Reply("What?! 9000?!");
		game.GetPlayerAttributes().IncreasePower(90.0f);
		return true;
	}

    public bool GiveItem(string[] arr)
    {
        if (arr.Length == 1)
        {
            string itemName = arr[0];
            try { game.GetPlayerAttributes().ApplyItem(game, ItemFactory.Create(itemName)); }
            catch { Reply(String.Format("{0} does not exist in lookup.", itemName)); }
        }
        else
        {
            Reply("GiveItem itemName");
        }
        return true;
    }

    public bool Strike(string[] arr)
    {
        string errorMessage = "Strike power [accuracy]";
        if (game.GetState() is IdleState && arr.Length < 3)
        {
            Powerbar powerbar = game.GetPowerbar();
            float power = 1f;
            float accuracy = 0f;
            if (arr.Length == 1) power = Floatify(arr[0]);
            if (arr.Length == 2) accuracy = Floatify(arr[1]);
            powerbar.SetPower(power);
            powerbar.SetAccuracy(accuracy);
            game.GetCursorGraphics().Disable();
            game.SetState(new StrikingState(game));
            return true;
        }
        Reply(errorMessage);
        return true;
    }

    public bool SimulatePower()
    {
        game.GetBall().SimulatePower();
        Reply("Done.");
        return true;
    }
    
    public bool WriteLeaderBoard()
    {
        return LeaderBoardController.WriteDummies();
    }

    #region utility
    public string[] Tail(string[] to) 
    {
        string[] o = new string[to.Length - 1];
        for (int i = 0; i < o.Length; i++)
            o[i] = to[i+1];
        return o;
    }
    
    public int Intify(string to) { return int.Parse(to); }
    public float Floatify(string to) { return float.Parse(to); }
    #endregion

}
