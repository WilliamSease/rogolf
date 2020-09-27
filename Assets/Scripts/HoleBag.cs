using System;
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

    private string holeName;
    private string holeListPath;
    private List<string> holeList;
    private List<HoleData> holesPlayed;

    public HoleBag()
    {
        holesPlayed = new List<HoleData>();
        holeListPath = ROGOLF_HOLES;
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
            holeName = holeList[index];
            // Remove hole from list
            holeList.RemoveAt(index);
            return holeName;
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

    public void AddHole(HoleData holeData) { holesPlayed.Add(holeData); }

    public int GetCurrentHoleNumber() { return holesPlayed.Count; }
    public HoleData GetCurrentHoleData() { 
        if (holesPlayed.Count > 0) return holesPlayed[holesPlayed.Count - 1];
        else return null;
    }
    public int GetHoleCount() { return holesPlayed.Count; }
    public List<HoleData> GetHolesPlayed() { return holesPlayed; }
}
