using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    public RawImage leftSelector;
    public RawImage rightSelector;

    private Text leftName;
    private Text leftDescription;
    private Text rightName;
    private Text rightDescription;
    private GameObject godObject;

    private Game game;
    private Item leftItem;
    private Item rightItem;

    public enum Selected { NONE, LEFT, RIGHT }
    private Selected selected;
    
    void Start()
    {
        // Turn selectors off
        leftSelector.gameObject.SetActive(false);
        rightSelector.gameObject.SetActive(false);

        // Get UI objects
        leftName = GameObject.Find("LeftName").GetComponent<Text>();
        leftDescription = GameObject.Find("LeftDescription").GetComponent<Text>();
        rightName = GameObject.Find("RightName").GetComponent<Text>();
        rightDescription = GameObject.Find("RightDescription").GetComponent<Text>();

        // Initialize logic
        godObject = GodObject.Create();
        godObject.AddComponent<Game>();
        game = godObject.GetComponent<Game>();
        game.enabled = false;
        game.LoadGameData();

        // Get items to display
        leftItem = game.GetItemBag().GetItem();
        rightItem = game.GetItemBag().GetItem();

        // Display items
        leftName.text = leftItem.GetName();
        leftDescription.text = leftItem.GetDescription();
        rightName.text = rightItem.GetName();
        rightDescription.text = rightItem.GetDescription();
        
        // Set selected item
        selected = Selected.NONE;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Set selected item
            selected = Selected.LEFT;
            leftSelector.gameObject.SetActive(true);
            rightSelector.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Set selected item
            selected = Selected.RIGHT;
            rightSelector.gameObject.SetActive(true);
            leftSelector.gameObject.SetActive(false);
        }

        if (selected != Selected.NONE && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
        {
            // Add and apply item
            game.GetItemBag().ApplyItem(game, selected == Selected.LEFT ? leftItem : rightItem);

            // Get next hole
            string nextHole = game.GetHoleBag().GetHole();

            // Save and destroy
            GameDataManager.SaveGameData(game);
            UnityEngine.Object.Destroy(godObject);

            // Advance to next hole
            GameController gc = GameObject.Find(GameController.NAME).GetComponent<GameController>();
            gc.LoadHole(nextHole);
        }
    }
}
