using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    public const string SCENE_NAME = "ItemScene";

    private const string LEFT_NAME = "LeftName";
    private const string LEFT_DESCRIPTION = "LeftDescription";
    private const string RIGHT_NAME = "RightName";
    private const string RIGHT_DESCRIPTION = "RightDescription";

    public RawImage leftSelector;
    public RawImage rightSelector;

    private Text leftName;
    private Text leftDescription;
    private Text rightName;
    private Text rightDescription;
    private GameObject gcObject;

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
        leftName = GameObject.Find(LEFT_NAME).GetComponent<Text>();
        leftDescription = GameObject.Find(LEFT_DESCRIPTION).GetComponent<Text>();
        rightName = GameObject.Find(RIGHT_NAME).GetComponent<Text>();
        rightDescription = GameObject.Find(RIGHT_DESCRIPTION).GetComponent<Text>();

        // Initialize logic
        gcObject = GameObject.Find(GameController.NAME);
        game = gcObject.GetComponent<Game>();

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
            game.GetPlayerAttributes().ApplyItem(game, selected == Selected.LEFT ? leftItem : rightItem);

            // Get next hole
            MoveOn();
        }
    }
	
	public void clickSelect(bool left, bool right)
	{
		if (left && right) return; //Shouldn't happen anyway but paranoid programmer
		if (left) game.GetPlayerAttributes().ApplyItem(game, leftItem);
		if (right) game.GetPlayerAttributes().ApplyItem(game, rightItem);
        BoomBox.Play(SoundEnum.Sound.CLICK);
		MoveOn();
	}
	
	void MoveOn()
	{
		string nextHole = game.GetHoleBag().GetHole();

            // Advance to next hole
            GameController gc = gcObject.GetComponent<GameController>();
            gc.LoadHole(nextHole);
	}
}
