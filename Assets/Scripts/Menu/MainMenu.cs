using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class MainMenu : MonoBehaviour
{
    public Canvas thisMenu;
    public Canvas optionsMenu;
    public Canvas controlsMenu;

    public Button button_1;
    public Button button_2;
    public Button button_3;
    public Button button_4;
    public Button button_5;
    public Button button_6;
    public Button button_7;
    public Button button_8;
    public Button button_9;

    int activeCharacter = 0;
    public Text characterSel;

    // Start is called before the first frame update
    void Start()
    {
        loadState();
        optionsMenu.enabled = false;
        controlsMenu.enabled = false;
        button_1.GetComponent<Button>().onClick.AddListener(task_1);
        button_2.GetComponent<Button>().onClick.AddListener(task_2);
        button_3.GetComponent<Button>().onClick.AddListener(task_3);
        button_4.GetComponent<Button>().onClick.AddListener(task_4);
        button_5.GetComponent<Button>().onClick.AddListener(task_5);
        button_6.GetComponent<Button>().onClick.AddListener(task_6);
        button_7.GetComponent<Button>().onClick.AddListener(task_7);
        /*button_8.GetComponent<Button>().onClick.AddListener(task_8);
        button_9.GetComponent<Button>().onClick.AddListener(task_9);*/
    }

    void task_1()
    {
        saveState();

        // Get GameController and start game
        GameObject gameObject = GameObject.Find(GameController.NAME);
        GameController gameController = gameObject.GetComponent<GameController>();
        gameController.StartGame();
    }

    void task_2()
    {
        saveState();
    }

    void task_3()
    {
        activeCharacter--;
        if (activeCharacter < 0) activeCharacter = 0;
        characterSel.text = "Placeholder Char " + activeCharacter;
    }

    void task_4()
    {
        activeCharacter++;
        characterSel.text = "Placeholder Char " + activeCharacter;
    }

    void task_5()
    {
        //UnityEngine.Debug.Log("Kicking to options menu...");
        saveState();
        optionsMenu.enabled = true;
    }

    void task_6()
    {
        //UnityEngine.Debug.Log("Kicking to controls menu...");
        saveState();
        controlsMenu.enabled = true;
    }

    void task_7()
    {
        Application.Quit();
    }

    void task_8() { }

    void task_9() { }

    void loadState()
    {
        activeCharacter = PlayerPrefs.GetInt("activeCharacter");
    }
    void saveState()
    {
        PlayerPrefs.SetInt("activeCharacter", activeCharacter);
    }
}