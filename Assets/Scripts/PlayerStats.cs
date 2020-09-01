using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public List<Hole> completedHoles;

    public PlayerStats()
    {
        completedHoles = new List<Hole>();

    }

    public void addCompletedHole(Hole hole)
    {
        completedHoles.Add(hole);
    }
}
