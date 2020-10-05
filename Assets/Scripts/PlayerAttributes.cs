using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAttributes
{
    private float power;
    private float control;
    private float impact;
    private float spin;

    // Default constructor
    public PlayerAttributes()
    {
        this.power = 0.20f;
        this.control = 0.20f;
        this.impact = 0.20f;
        this.spin = 0.20f;
    }

    // Constructor for when values are known
    public PlayerAttributes(float power, float control, float impact, float spin)
    {
        this.power = power;
        this.control = control;
        this.impact = impact;
        this.spin = spin;
    }

    public float GetPower() { return power; }
    public float GetControl() { return control; }
    public float GetImpact() { return impact; }
    public float GetSpin() { return spin; }

    public void SetPower(float power) { this.power = power; }
    public void SetControl(float control) { this.control = control; }
    public void SetImpact(float impact) { this.impact = impact; }
    public void SetSpin(float spin) { this.spin = spin; }

    public void IncreasePower(float increase) { this.power += increase; }
    public void IncreaseControl(float increase) { this.control += increase; }
    public void IncreaseImpact(float increase) { this.impact += increase; }
    public void IncreaseSpin(float increase) { this.spin += increase; }

    public override string ToString()
    {
        return String.Format("P:{0}, C:{1}, I:{2}, S:{3}", power, control, impact, spin);
    }
}
