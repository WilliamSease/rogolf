using GameModeEnum;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameModeEnum
{
    public enum GameMode
    {
        ROGOLF = 0,
        RANGE = 2
    }
}

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
    
    public Text[] statText = new Text[4];
    public Text presetText;
    private readonly string[] presetNames = new string[]{"Balanced", "Beginner", "Power", "Skill"};
    private readonly int[] powValues = new int[]{20,10,40,20};
    private readonly int[] conValues = new int[]{20,20,10,25};
    private readonly int[] impValues = new int[]{20,30,10, 5};
    private readonly int[] spiValues = new int[]{20,10,10,30};

    private int activeCharacter = 0;

    // Start is called before the first frame update
    void Start()
    {
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
        activeCharacter = 0;
        updateCharacterText();
    }

    void task_1()
    {
        StartGame(GameMode.ROGOLF);
    }

    void task_2()
    {
        StartGame(GameMode.RANGE);
    }

    private void StartGame(GameMode gameMode)
    {
        BoomBox.Play(SoundEnum.Sound.CLICK);

        // Get GameController and start game
        GameObject gameObject = GameObject.Find(GameController.NAME);
        GameController gameController = gameObject.GetComponent<GameController>();
        gameController.StartGame(gameMode);

        // Set PlayerAttributes according to preset
        PlayerAttributes p = gameController.GetComponent<Game>().GetPlayerAttributes();
        p.SetPower((float)powValues[activeCharacter]/100f);
        p.SetControl((float)conValues[activeCharacter]/100f);
        p.SetImpact((float)impValues[activeCharacter]/100f);
        p.SetSpin((float)spiValues[activeCharacter]/100f);
    }

    void task_3()
    {
        BoomBox.Play(SoundEnum.Sound.CLICK);
        activeCharacter--;
        if (activeCharacter < 0) activeCharacter = 0;
        updateCharacterText();
    }

    void task_4()
    {
        BoomBox.Play(SoundEnum.Sound.CLICK);
        activeCharacter++;
        if (activeCharacter >= powValues.Length) activeCharacter = powValues.Length - 1;
        updateCharacterText();
    }

    void task_5()
    {
        BoomBox.Play(SoundEnum.Sound.CLICK);
        optionsMenu.enabled = true;
    }

    void task_6()
    {
        BoomBox.Play(SoundEnum.Sound.CLICK);
        controlsMenu.enabled = true;
    }

    void task_7()
    {
        BoomBox.Play(SoundEnum.Sound.CLICK);
        Application.Quit();
    }

    void task_8() 
    {
        BoomBox.Play(SoundEnum.Sound.CLICK);
    }

    void task_9() 
    {
        BoomBox.Play(SoundEnum.Sound.CLICK);
    }
    
    void updateCharacterText()
    {
        statText[0].text = powValues[activeCharacter].ToString();
        statText[1].text = conValues[activeCharacter].ToString(); 
        statText[2].text = impValues[activeCharacter].ToString(); 
        statText[3].text = spiValues[activeCharacter].ToString();
        presetText.text = presetNames[activeCharacter];
    }
}