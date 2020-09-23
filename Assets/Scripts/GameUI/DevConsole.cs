using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class DevConsole : MonoBehaviour
{
	public GameController gc;
	
	public Canvas thisCanvas;
	public Text[] bodyText = new Text[20];
	public InputField inputField;
	public Text inputText;
	private int memory;
	public string[] splash = 
	{
		"Rogolf 2020 William Sease & Matthew Swanson",
		"Try \"Help\"",
	};
	public string[] helpMessage =
	{
		"Help: Displays this message",
		"Scene [string]: Attempt to load a scene (unstable)",
		"Status: Display interesting things.",
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
		Pump(str);
		string[] arr = Regex.Split(str.ToLower(), " ");
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
}
