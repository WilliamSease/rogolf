using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBag
{
    private List<string> holeList;

    public HoleBag()
    {
        NewHoleList();
    }

    public string GetHole()
    {
        // If holeList is empty, create a new one and try again
        if (holeList.Count == 0)
        {
            NewHoleList();
            return GetHole();
        }
        else
        {
            // Get random hole
            int index = 0;
            string hole = holeList[index];
            // Remove hole from list
            holeList.RemoveAt(index);
            return hole;
        }
    }

    /// <summary>
    /// Generates a full hole list with all possible holes
    /// </summary>
    private void NewHoleList()
    {
        holeList = new List<string>();
        holeList.Add("a01");
    }
}
