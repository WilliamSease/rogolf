using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class ShopController : MonoBehaviour
{
    private const int ROWS = 3;
    private const int COLS = 3;
    private Game game;

    private Score score;
    private PlayerAttributes playerAttributes;
    private ItemBag itemBag;
    private ItemBag badItemBag;
    public const string SCENE_NAME = "ShopScene";
    public Button[] positives = new Button[ROWS * COLS];
    public Text[] negatives = new Text[ROWS * COLS];
    private Item[] positiveItems = new Item[ROWS * COLS];
    private Item[] negativeItems = new Item[COLS];
    private Club[] clubTrades = new Club[COLS];
    public RawImage[] pluses = new RawImage[ROWS * COLS];
    public RawImage[] botBGs = new RawImage[COLS];
    public GameObject[] hoverExplanations = new GameObject[ROWS * COLS];
    public Texture checkmark;
    private int SUB_CREDITS_AMOUNT = 300;
    public Text creditsText;
    
    void Start()
    {
        game = GameObject.Find(GameController.NAME).GetComponent<Game>();

        score = game.GetScore();
        playerAttributes = game.GetPlayerAttributes();
        itemBag = game.GetItemBag();
        badItemBag = game.GetBadItemBag();

        // Set club trades
        clubTrades = game.GetBag().GetRandomClubs(COLS).ToArray();

        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                SetItem(i, j, itemBag.GetItem());
                WritePositive(i, j, GetItem(i, j).GetName()); 
                WritePositiveExplanation(i, j, GetItem(i, j).GetDescription());
                    
                if (i == 0) 
                {
                    WriteNegative(i, j, SUB_CREDITS_AMOUNT.ToString()); 
                    WriteNegativeExplanation(i, j, String.Format("Costs {0} credits", SUB_CREDITS_AMOUNT));
                }
                if (i == 1)
                {
                    WriteNegative(i, j, clubTrades[j].GetName());
                    WriteNegativeExplanation(i, j, String.Format("Lose {0}", clubTrades[j].GetName()));
                }
                if (i == 2)
                {
                    SetBadItem(j, badItemBag.GetItem());
                    WriteNegative(i, j, GetBadItem(j).GetName()); 
                    WriteNegativeExplanation(i, j, GetBadItem(j).GetDescription());
                }
                int x = i; int y = j;
                positives[GetIndex(i, j)].onClick.AddListener(() => Clicked(x,y));
            }
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(ItemSelector.SCENE_NAME);
        creditsText.text = score.GetCredits().ToString();
    }
    
    void Clicked(int row, int column)
    {
        if (row == 0 && score.GetCredits() >= SUB_CREDITS_AMOUNT)
        {
            score.AddDebit(SUB_CREDITS_AMOUNT);
            MarkChecked(row, column);
            playerAttributes.ApplyItem(game, GetItem(row, column));
        }
        else if (row == 1)
        {
            game.GetBag().RemoveClub(clubTrades[column]);
            game.GetBag().DecrementBag();
            MarkChecked(row, column);
            playerAttributes.ApplyItem(game, GetItem(row, column));
        }
        else if (row == 2)
        {
            playerAttributes.ApplyItem(game, GetItem(row, column));
            playerAttributes.ApplyItem(game, GetBadItem(column));
            MarkChecked(row, column);
        }
    }
    
    void WritePositive(int row, int column, string text) { positives[GetIndex(row, column)].GetComponentInChildren<Text>().text = text; }
    
    void WriteNegative(int row, int column, string text) { negatives[GetIndex(row, column)].text = text; }
    
    void SetItem(int row, int column, Item item){ positiveItems[GetIndex(row, column)] = item; }
    Item GetItem(int row, int column) { return positiveItems[GetIndex(row, column)]; }
    void SetBadItem(int column, Item item){ negativeItems[column] = item; }
    Item GetBadItem(int column) { return negativeItems[column]; } 

    /// <summary>
    /// Disables a button and displays appropriate green check mark.
    /// </summary>
    void MarkChecked(int row, int column)
    {
        positives[GetIndex(row, column)].interactable = false;
        pluses[GetIndex(row, column)].texture = checkmark;
        if (row == 2) botBGs[column].color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
    }
    
    void WritePositiveExplanation(int row, int column, string text) { hoverExplanations[GetIndex(row, column)].transform.GetChild(1).GetComponent<Text>().text = text; }
    void WriteNegativeExplanation(int row, int column, string text) { hoverExplanations[GetIndex(row, column)].transform.GetChild(2).GetComponent<Text>().text = text; }

    int GetIndex(int row, int column) { return row * COLS + column; }
}
