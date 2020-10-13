using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
public class ShopController : MonoBehaviour
{
    public Button[] positives = new Button[9];
    public Text[] negatives = new Text[9];
    public RawImage[] pluses = new RawImage[9];
    public RawImage[] botBGs = new RawImage[3];
    public GameObject[] hoverExplanations = new GameObject[9];
    public Texture checkmark;
    private int credits;
    public Text creditsText;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                int x = i;
                int y = j;
                positives[i * 3 + j].onClick.AddListener(() => clicked(x,y)); //What a monstrosity...
            }
    }
    
    void Update()
    {
        creditsText.text = credits + ""; //credits is updated every frame. I'll leave the implemenation in the air.
    }
    
    void clicked (int row, int column) //I don't know what needs to happen here.
    {
        markChecked(row, column);
    }
    
    void writePositive(int row, int column, string text) { positives[row * 3 + column].GetComponentInChildren<Text>().text = text; }
    
    void writeNegative(int row, int column, string text) { negatives[row * 3 + column].text = text; }
    
    void markChecked(int row, int column) //Disables a button and displays appropriate green checkmark.
    {
        positives[row * 3 + column].interactable = false;
        pluses[row * 3 + column].texture = checkmark;
        if (row == 2) botBGs[column].color = new Color(1.0f,1.0f,1.0f,0.4f);
    }
    
    void writePositiveExplanation(int row, int column, string text) { hoverExplanations[row * 3 + column].transform.GetChild(1).GetComponent<Text>().text = text; }
    void writeNegativeExplanation(int row, int column, string text) { hoverExplanations[row * 3 + column].transform.GetChild(2).GetComponent<Text>().text = text; }

    
    int getCredits() { return credits; }
    void setCredits(int credits) { this.credits = credits; }
}
