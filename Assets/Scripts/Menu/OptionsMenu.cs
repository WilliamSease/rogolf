using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class OptionsMenu : MonoBehaviour
{
    public Canvas thisMenu;
	public Slider volumeSlider;
	public Text volumeSliderText;
	public Slider panSlider;
    public Text panSliderText;
    public Slider rotSlider;
    public Text rotSliderText;

    public Button button_1;
    public Button button_2;
    public Button button_3;
    public Button button_4;
    public Button button_5;
    public Button button_6;
    public Button button_7;
    public Button button_8;
    public Button button_9;

    // Start is called before the first frame update
    void Start()
    {
        if (thisMenu.enabled)
            thisMenu.enabled = false;
        button_1.GetComponent<Button>().onClick.AddListener(task_1);
        button_2.GetComponent<Button>().onClick.AddListener(task_2);
        button_3.GetComponent<Button>().onClick.AddListener(task_3);
        /*button_4.GetComponent<Button>().onClick.AddListener(task_4);
        button_5.GetComponent<Button>().onClick.AddListener(task_5);
        button_6.GetComponent<Button>().onClick.AddListener(task_6);
        button_7.GetComponent<Button>().onClick.AddListener(task_7);
        button_8.GetComponent<Button>().onClick.AddListener(task_8);
        button_9.GetComponent<Button>().onClick.AddListener(task_9);*/
    }
	
	void Update()
	{
		volumeSliderText.text = (int) (volumeSlider.value * 100f) + "%";
        panSliderText.text = (int) (panSlider.value * 100f) + "%";
        rotSliderText.text = (int) (rotSlider.value * 100f) + "%";
	}

    void task_1()
    {
        //UnityEngine.Debug.Log("Back to main...");
        thisMenu.enabled = false;
    }

    void task_2()
    {
		//Save Settings here...
		BoomBox.SetVolumeStat(volumeSlider.value);
        MouseOrbitImproved.SetMouseSensitivity(rotSlider.value);
        GameObject.Find("GameController").GetComponent<Game>().SetPanSensitivity(panSlider.value);
    }

    void task_3()
    {
		//Volume Test...
		BoomBox.Play(SoundEnum.Sound.TEST);
    }

    void task_4()
    {
    }

    void task_5()
    {
    }

    void task_6()
    {
    }

    void task_7()
    {
    }

    void task_8()
    {
    }

    void task_9()
    {
    }
}