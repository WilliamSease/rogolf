using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class TerrainType
{
    /// <summary>
    /// Friction coefficient from [0, 1].
    /// </summary>
    private double friction;

    /// <summary>
    /// Bounce coefficient from [0, 1].
    /// </summary>
    private double bounce;

    /// <summary>
    /// Lie coefficient from [0, 1].
    /// </summary>
    private double lieRate;

    /// <summary>
    /// TOTAL vaiability in lie.
    /// For example, a lie rate of 0.8 and a lie range of 0.1 produces lies from [0.75, 0.85].
    /// </summary>
    private double lieRange;

    public TerrainType(double friction, double bounce, double lieRate, double lieRange)
    {
        this.friction = friction;
        this.bounce = bounce;
        this.lieRate = lieRate;
        this.lieRange = lieRange;
    }

    public double GetFriction() { return friction; }
    public double GetBounce() { return bounce; }
    public double GetLie()
    {
        // This should return an evenly distributed lie depending on the lieRange
        return lieRate;
    }

    public void SetFriction(double friction) { this.friction = friction; }
    public void SetBounce(double bounce) { this.bounce = bounce; }
    public void SetLieRate(double lieRate) { this.lieRate = lieRate; }
    public void SetLieRange(double lieRange) { this.lieRange = lieRange; }

}
