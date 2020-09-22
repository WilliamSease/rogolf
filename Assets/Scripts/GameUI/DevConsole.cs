using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DevConsole : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 19; i++)
			bodyText[i].text = "";
		for(int i = 0; i < splash.Length; i++)
			bodyText[bodyText.Length - splash.Length + i].text = splash[i];
		thisCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		inputField.text = inputField.text.Replace("`", string.Empty);
        if (Input.GetKeyDown(KeyCode.BackQuote))
			thisCanvas.enabled = !thisCanvas.enabled;
		if(!thisCanvas.enabled)
		{
			inputField.DeactivateInputField();
			inputField.text = "";
			memory = 20;
			return;
		}
		inputField.Select();
		inputField.ActivateInputField();
		if (Input.GetKeyDown(KeyCode.Return))
		{
			execute(inputText.text);
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
	
	void pump(string str)
	{
		for(int i = 0; i < 19; i++)
			bodyText[i].text = bodyText[i+1].text;
		bodyText[19].text = str;
	}
	
	void execute(string str)
	{
		string[] arr = Regex.Split(str.ToLower(), " ");
		for(int i = 0; i < arr.Length; i++)
			UnityEngine.Debug.Log(arr[i]);
	}
	
	public void Scene()
	{
		pump("Holy shit!");
	}
}
