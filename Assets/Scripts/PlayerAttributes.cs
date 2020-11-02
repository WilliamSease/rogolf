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
    
    private int credits;
    private int debits;
    
    private List<Item> heldItems;

    // Default constructor
    public PlayerAttributes()
    {
        heldItems = new List<Item>();
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

    public void SetPower(float power) { this.power = Mathf.Clamp(power, 0f, 1f); }
    public void SetControl(float control) { this.control = Mathf.Clamp(control, 0f, 1f); }
    public void SetImpact(float impact) { this.impact = Mathf.Clamp(impact, 0f, 1f); }
    public void SetSpin(float spin) { this.spin = Mathf.Clamp(spin, 0f, 1f); }

    public void IncreasePower(float increase) { SetPower(power + increase); }
    public void IncreaseControl(float increase) { SetControl(control + increase); }
    public void IncreaseImpact(float increase) { SetImpact(impact + increase); }
    public void IncreaseSpin(float increase) { SetSpin(spin + increase); }

    public override string ToString()
    {
        return String.Format("P:{0}, C:{1}, I:{2}, S:{3}", power, control, impact, spin);
    }
    
    public void Earn(int n)
    {
        credits += n;
    }
    
    public void Spend(int n)
    {
        credits -= n;
        debits += n;
    }
    
    public int TotalEarnings() { return credits + debits; }
    public int GetCredits() { return credits; }
    
    public void ApplyItem(Game game, Item item)
    {
        item.Apply(game.GetPlayerAttributes(), game.GetTerrainAttributes());
        heldItems.Add(item);
    }
    
    public List<Item> GetHeldItems() { return heldItems; }
}
