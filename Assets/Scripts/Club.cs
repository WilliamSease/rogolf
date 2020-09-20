using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club
{
    private string name;
    private float power;
    private float loft;
    private float distance;

    public Club(string name, float power, float loft)
    {
        this.name = name;
        this.power = power;
        this.loft = loft;
        // Distance gets simulated by the bag
        this.distance = Single.NaN;
    }

    public string GetName() { return name; }
    public float GetPower() { return power; }
    public float GetLoft() { return loft; }
    public float GetDistance()
    {
        if (distance == Single.NaN) { 
            throw new InvalidOperationException("Can't get club distance before we've calculated it!");
        }
        return distance;
    }

    public void SetPower(float power) { this.power = power; }
    public void SetLoft(float loft) { this.loft = loft; }
    public void SetDistance(float distance) { this.distance = distance; }
}
