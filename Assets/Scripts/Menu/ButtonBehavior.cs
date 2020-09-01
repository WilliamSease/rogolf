using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class ButtonBehavior : MonoBehaviour
{
    public Button buttonOne;
    public InputField inField;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = buttonOne.GetComponent<Button>();
        btn.onClick.AddListener(taskOne);

    }

    void taskOne()
    {
        PlayerPrefs.SetString("key", inField.text);
        SceneManager.LoadScene("SampleScene");
    }

    void taskTwo()
    {
        UnityEngine.Debug.Log("Eat shit.");
    }

    void taskThree()
    {
        UnityEngine.Debug.Log("No.");
    }
}
