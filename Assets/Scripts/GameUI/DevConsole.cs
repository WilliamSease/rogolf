using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class DevConsole : MonoBehaviour
{
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
	{	"***************************",
		"Help: Displays this message",
		"Scene [string]: Attempt to load a scene (unstable)",
		"Status: Display interesting things.",
		"MoveBall [\"Abs\" \"Rel\"] [f] [f] [f]: Places the ball.",
		"GetBallPos: Prints the ball's position.",
		"***************************"
	};
	
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 19; i++)
			Pump("");
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
	
	void Execute(string str)
	{
		try { game = GameObject.Find("GodObject").GetComponent<Game>(); }
		catch (NullReferenceException e) { Reply("Game is currently null. Functionality is limited!"); }
		Pump(str);
		string[] arr = Regex.Split(str, " ");
		arr[0] = arr[0].ToLower();
		if (arr.Length == 0) return;
		switch(arr[0])
		{
			case "help":
				PumpArr(helpMessage);
			break;
			case "scene":
				Scene(arr[1]);
			break;
			case "status":
				Status();
			break;
			case "absmov":
				AbsMov(Tail(arr));
			break;
			case "moveball":
				MoveBall(arr[1].ToLower(), Floatify(arr[2]), Floatify(arr[3]), Floatify(arr[4]));
			break;
			case "getballpos":
				GetBallPos();
			break;
			default:
				Reply("'" + arr[0] + "' dosen't appear to be a command");
			break;
		}
	}
	
	public void Scene(string str)
	{
		Reply("Attempting Scene Load...");
		gc.LoadScene(str);
	}
	
	public void Status()
	{
		Reply("***STATUS***");
		Pump("Memory Usage: " + System.GC.GetTotalMemory(true) + "Bytes");
	}
	
	public void MoveBall(string type, float x, float y, float z)
	{
		if (type.Equals("abs")) 
			game.GetBall().SetPosition(new Vector3(x,y,z));
		else if (type.Equals("rel"))
			game.GetBall().SetRelativePosition(x,y,z);
		else 
			Reply("MoveBall: You must specify 'abs' or 'rel'!");
	}
	
	public void GetBallPos() 
	{ 
		Vector3 v = game.GetBall().GetPosition();
		Reply("GetBallPos: " + v[0] + " " + v[1] + " " + v[2]);
	}
	
	public void AbsMov(string[] arr)
	{
		GameObject g = GameObject.Find(arr[0]);
		if (g == null) Reply("GameObject " + arr[0] + " appears not to exist.");
		else g.transform.position = new Vector3(Floatify(arr[1]),Floatify(arr[2]),Floatify(arr[3]));
	}
	
	//These are easy utility functions.
	public string[] Tail(string[] to) 
	{
		string[] o = new string[to.Length - 1];
		for (int i = 0; i < o.Length; i++)
			o[i] = to[i+1];
		return o;
	}
	
	public int Intify(string to) { return int.Parse(to); }
	public float Floatify(string to) { return float.Parse(to); } 

}
