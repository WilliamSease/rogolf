using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club
{
    private string name;
    private float power;
    private float loft;

    public Club(string name, float power, float loft)
    {
        this.name = name;
        this.power = power;
        this.loft = loft;
    }

    public string GetName() { return name; }
    public float GetPower() { return power; }
    public float GetLoft() { return loft; }

    public void SetPower(float power) { this.power = power; }
    public void SetLoft(float loft) { this.loft = loft; }
}
