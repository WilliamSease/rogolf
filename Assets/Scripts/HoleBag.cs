using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public class HoleBag
{
    public const string PREFIX = ".\\Assets\\Data\\";

    public const string ROGOLF_HOLES = "rogolf_holes.xml";
    public const string TEST_HOLES = "test_holes.xml";
    public const string RANGE_HOLES = "range_holes.xml";

    private string holeName;
    private string holeListPath;
    private List<HoleItem> holeList;
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
            string hl = "";
            foreach (HoleItem i in holeList) { hl += i.name+", "; }
            UnityEngine.Debug.Log(hl);
            // Get random hole
            int index = UnityEngine.Random.Range(0, holeList.Count);
            holeName = holeList[index].name;
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
        try
        {
            XDocument xml = XDocument.Load(PREFIX + holeListPath);
            holeList = (from holeItem in xml.Root.Elements("hole")
                        select new HoleItem()
                        {
                            name = (string) holeItem.Element("name"),
                            hcp = (float) holeItem.Element("hcp"),
                        }).ToList();
        }
        catch
        {
            throw new Exception(String.Format("Hole list parse error ({0})", holeListPath));
        }
    }

    public void AddHole(HoleData holeData) { holesPlayed.Add(holeData); }

    public int GetCurrentHoleNumber() { return holesPlayed.Count; }
    public HoleData GetCurrentHoleData()
    {
        return holesPlayed.Count > 0 ? holesPlayed[holesPlayed.Count - 1] : null;
    }
    public int GetHoleCount() { return holesPlayed.Count; }
    public List<HoleData> GetHolesPlayed() { return holesPlayed; }
}

public class HoleItem
{
    public string name { get; set; }
    public float hcp { get; set; }
}
