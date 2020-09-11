using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemBag
{
    private List<string> itemList;

    public ItemBag()
    {
        NewItemList();
    }

    public string GetHole()
    {
        // If holeList is empty, create a new one and try again
        if (itemList.Count == 0)
        {
            NewItemList();
            return GetHole();
        }
        else
        {
            // Get random hole
            int index = 0;
            string hole = itemList[index];
            // Remove hole from list
            itemList.RemoveAt(index);
            return hole;
        }
    }

    /// <summary>
    /// Generates a full hole list with all possible holes
    /// </summary>
    private void NewItemList()
    {
        itemList = new List<string>();
        itemList.Add("a01");
    }
}
