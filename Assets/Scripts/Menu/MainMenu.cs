﻿using System.Collections;
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

    public GameObject godObject;

    int activeCharacter = 0;
    public Text characterSel;

    // Start is called before the first frame update
    void Start()
    {
        loadState();
        button_1.GetComponent<Button>().onClick.AddListener(task_1);
        button_2.GetComponent<Button>().onClick.AddListener(task_2);
        button_3.GetComponent<Button>().onClick.AddListener(task_3);
        button_4.GetComponent<Button>().onClick.AddListener(task_4);
        button_5.GetComponent<Button>().onClick.AddListener(task_5);
        button_6.GetComponent<Button>().onClick.AddListener(task_6);
        button_7.GetComponent<Button>().onClick.AddListener(task_7);
        button_8.GetComponent<Button>().onClick.AddListener(task_8);
        button_9.GetComponent<Button>().onClick.AddListener(task_9);
        optionsMenu.enabled = false;
        controlsMenu.enabled = false;
    }

    void task_1()
    {
        UnityEngine.Debug.Log("Playing rogolf...");
        saveState();
        //StartGame.Start();
        Instantiate(godObject);
    }

    void task_2()
    {
        UnityEngine.Debug.Log("Playing golf...");
        //SceneManager.LoadScene("SampleScene");
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
        thisMenu.enabled = false;
        optionsMenu.enabled = true;
    }

    void task_6()
    {
        //UnityEngine.Debug.Log("Kicking to controls menu...");
        saveState();
        thisMenu.enabled = false;
        controlsMenu.enabled = true;
    }

    void task_7()
    {
        Application.Quit();
    }

    void task_8()
    {
    }

    void task_9()
    {
    }

    void loadState()
    {
        activeCharacter = PlayerPrefs.GetInt("activeCharacter");
    }
    void saveState()
    {
        PlayerPrefs.SetInt("activeCharacter", activeCharacter);
    }
}