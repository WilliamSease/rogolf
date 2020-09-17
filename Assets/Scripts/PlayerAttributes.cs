using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAttributes
{
    private double power;
    private double control;
    private double impact;
    private double spin;

    // Default constructor
    public PlayerAttributes()
    {
        this.power = 30.0;
        this.control = 30.0;
        this.impact = 30.0;
        this.spin = 30.0;
    }

    // Constructor for when values are known
    public PlayerAttributes(double power, double control, double impact, double spin)
    {
        this.power = power;
        this.control = control;
        this.impact = impact;
        this.spin = spin;
    }

    public double GetPower() { return power; }
    public double GetControl() { return control; }
    public double GetImpact() { return impact; }
    public double GetSpin() { return spin; }

    public void SetPower(double power) { this.power = power; }
    public void SetControl(double control) { this.control = control; }
    public void SetImpact(double impact) { this.impact = impact; }
    public void SetSpin(double spin) { this.spin = spin; }

    public void IncreasePower(double increase) { this.power += increase; }
    public void IncreaseContro(double increase) { this.control += increase; }
    public void IncreaseImpact(double increase) { this.impact += increase; }
    public void IncreaseSpin(double increase) { this.spin += increase; }
}
