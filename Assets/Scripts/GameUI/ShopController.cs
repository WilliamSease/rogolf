using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
public class ShopController : MonoBehaviour
{
    private Game gameRef;
    private PlayerAttributes plrAttr;
    private ItemBag itemBag;
    public const string SCENE_NAME = "ShopScene";
    public Button[] positives = new Button[9];
    public Text[] negatives = new Text[9];
    private Item[] positiveItems = new Item[9];
    private Club[] clubTrades = new Club[3];
    public RawImage[] pluses = new RawImage[9];
    public RawImage[] botBGs = new RawImage[3];
    public GameObject[] hoverExplanations = new GameObject[9];
    public Texture checkmark;
    private int SUB_CREDITS_AMOUNT = 3000;
    public Text creditsText;
    
    // Start is called before the first frame update
    void Start()
    {
        gameRef = GameObject.Find("GameController").GetComponent<Game>();
        plrAttr = gameRef.GetPlayerAttributes();
        itemBag = gameRef.GetItemBag();
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                SetItem(i, j, itemBag.GetItem());
                writePositive(i, j, GetItem(i, j).GetName()); 
                writePositiveExplanation(i, j, GetItem(i, j).GetDescription());
                    
                if (i == 0) 
                {
                    writeNegative(i, j, SUB_CREDITS_AMOUNT + ""); 
                    writeNegativeExplanation(i, j, "Costs " + SUB_CREDITS_AMOUNT + " Credits.");
                }
                if (i == 1)
                {
                    SELECT:
                    clubTrades[j] = gameRef.GetBag().GetRandomClub();
                    for (int k = j - 1; k >= 0; k--) if (clubTrades[j] == clubTrades[k]) goto SELECT; //NO duplicate clubs.
                    writeNegative(i, j, clubTrades[j].GetName() + "");
                    writeNegativeExplanation(i, j, "Lose Your " + clubTrades[j].GetName() + ".");
                }
                if (i == 2)
                {
                    
                }
                int x = i; int y = j;
                positives[i * 3 + j].onClick.AddListener(() => clicked(x,y)); //Ay Dios Mio...
            }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(ItemSelector.SCENE_NAME);
        creditsText.text = plrAttr.GetCredits() + ""; //credits is updated every frame. I'll leave the implemenation in the air.
    }
    
    void clicked (int row, int column) //I don't know what needs to happen here.
    {
        if (row == 0 && plrAttr.GetCredits() >= SUB_CREDITS_AMOUNT)
        {
            plrAttr.Spend(SUB_CREDITS_AMOUNT);
            markChecked(row, column);
            plrAttr.ApplyItem(gameRef, GetItem(row, column));
        }
        if (row == 1)
        {
            gameRef.GetBag().RemoveClub(clubTrades[column]);
            markChecked(row, column);
            plrAttr.ApplyItem(gameRef, GetItem(row, column));
        }
        if (row == 2)
        {
            plrAttr.ApplyItem(gameRef, GetItem(row, column));
        }
    }
    
    void writePositive(int row, int column, string text) { positives[row * 3 + column].GetComponentInChildren<Text>().text = text; }
    
    void writeNegative(int row, int column, string text) { negatives[row * 3 + column].text = text; }
    
    void SetItem(int row, int column, Item item){ positiveItems[row * 3 + column] = item; }
    Item GetItem(int row, int column) { return positiveItems[row * 3 + column]; } 

    
    void markChecked(int row, int column) //Disables a button and displays appropriate green checkmark.
    {
        positives[row * 3 + column].interactable = false;
        pluses[row * 3 + column].texture = checkmark;
        if (row == 2) botBGs[column].color = new Color(1.0f,1.0f,1.0f,0.4f);
    }
    
    void writePositiveExplanation(int row, int column, string text) { hoverExplanations[row * 3 + column].transform.GetChild(1).GetComponent<Text>().text = text; }
    void writeNegativeExplanation(int row, int column, string text) { hoverExplanations[row * 3 + column].transform.GetChild(2).GetComponent<Text>().text = text; }
}
