using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemBag
{
    private List<Item> itemList;
    private List<Item> heldItems;

    public ItemBag()
    {
        heldItems = new List<Item>();
        NewItemList();
    }

    public Item GetItem()
    {
        // If itemList is empty, create a new one and try again
        if (itemList.Count == 0)
        {
            NewItemList();
            return GetItem();
        }
        else
        {
            // Get random item
            int index = 0;
            Item item = itemList[index];
            // Remove hole from list
            itemList.RemoveAt(index);
            return item;
        }
    }

    public void ApplyItem(Game game, Item item)
    {
        item.Apply(game.GetPlayerAttributes(), game.GetTerrainAttributes());
        heldItems.Add(item);
    }

    /// <summary>
    /// Generates a full item list with all possible items
    /// </summary>
    private void NewItemList()
    {
        // Make new empty list
        itemList = new List<Item>();

        // Add items
        itemList.Add(new PowerUp());
        itemList.Add(new ControlUp());
        itemList.Add(new ImpactUp());
        itemList.Add(new SpinUp());
    }

    public List<Item> GetHeldItems()
    {
        return heldItems;
    }
}
