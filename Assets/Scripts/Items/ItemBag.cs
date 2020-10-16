using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public class ItemBag
{
    public const string PREFIX = ".\\Assets\\Data\\";

    public const string ITEMS = "items.xml";

    private string itemListPath;
    private List<Item> itemList;
    private List<Item> heldItems;

    public ItemBag()
    {
        heldItems = new List<Item>();
        itemListPath = ITEMS;
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
            int index = UnityEngine.Random.Range(0, itemList.Count);
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
        try
        {
            XDocument xml = XDocument.Load(PREFIX + itemListPath);
            List<string> stringList = (from itemName in xml.Root.Elements("item") select (string) itemName).ToList();
            itemList = (from itemName in stringList select ItemFactory.Create(itemName)).ToList();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
            throw new Exception(String.Format("Item list parse error ({0})", itemListPath));
        }
    }

    public List<Item> GetHeldItems() { return heldItems; }
}
