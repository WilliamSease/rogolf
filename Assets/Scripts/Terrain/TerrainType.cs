using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class TerrainType
{
    /// <summary>
    /// Friction coefficient from [0, 1].
    /// </summary>
    private float friction;

    /// <summary>
    /// Bounce coefficient from [0, 1].
    /// </summary>
    private float bounce;

    /// <summary>
    /// Lie coefficient from [0, 1].
    /// </summary>
    private float lieRate;

    /// <summary>
    /// TOTAL vaiability in lie.
    /// For example, a lie rate of 0.8 and a lie range of 0.1 produces lies from [0.75, 0.85].
    /// </summary>
    private float lieRange;

    public TerrainType(float friction, float bounce, float lieRate, float lieRange)
    {
        this.friction = friction;
        this.bounce = bounce;
        this.lieRate = lieRate;
        this.lieRange = lieRange;
    }

    public float GetFriction() { return friction; }
    public float GetBounce() { return bounce; }
    public float GetLie()
    {
        // This should return an evenly distributed lie depending on the lieRange
        return lieRate;
    }

    public void SetFriction(float friction) { this.friction = friction; }
    public void SetBounce(float bounce) { this.bounce = bounce; }
    public void SetLieRate(float lieRate) { this.lieRate = lieRate; }
    public void SetLieRange(float lieRange) { this.lieRange = lieRange; }

}
