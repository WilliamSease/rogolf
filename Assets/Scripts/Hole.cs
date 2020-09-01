using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole
{
    private string name;
    private int strokes;

    // Maybe associated data about the hole that we load?

    public Hole(string name)
    {
        this.name = name;
        strokes = 0;

    }

    public void incrementStroke()
    {
        strokes++;
    }
    
    public string getName()
    {
        return name;
    }
}
