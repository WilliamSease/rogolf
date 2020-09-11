using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class HoleBag
{
    public const string PREFIX = ".\\Assets\\Data\\";

    public const string ROGOLF_HOLES = "rogolf_holes.txt";
    public const string TEST_HOLES = "test_holes.txt";

    private string holeListPath;
    private List<string> holeList;

    public HoleBag()
    {
        holeListPath = TEST_HOLES;
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
        var lines = File.ReadLines(PREFIX + holeListPath);
        foreach (var line in lines)
        {
            holeList.Add(line);
        }
    }
}
