using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public double getPower()
    {
        return power;
    }

    public double getControl()
    {
        return control;
    }

    public double getImpact()
    {
        return impact;
    }

    public double getSpin()
    {
        return spin;
    }

    public void setPower(double power)
    {
        this.power = power;
    }

    public void setControl(double control)
    {
        this.control = control;
    }

    public void setImpact(double impact)
    {
        this.impact = impact;
    }

    public void setSpin(double spin)
    {
        this.spin = spin;
    }
}
