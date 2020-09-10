using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club
{
    private string name;
    private double power;
    private double loft;

    public Club(string name, double power, double loft)
    {
        this.name = name;
        this.power = power;
        this.loft = loft;
    }

    public string GetName() { return name; }
    public double GetPower() { return power; }
    public double GetLoft() { return loft; }

    public void SetPower(double power) { this.power = power; }
    public void SetLoft(double loft) { this.loft = loft; }
}
