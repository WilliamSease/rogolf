using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PerformanceTees : Item
{
    public PerformanceTees() {
        this.name = "PerformanceTees";
        this.description = "Increase tee power";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        TerrainType tee = terrainAttributes.GetTeeTerrain();
        tee.SetLieRate(tee.GetLieRate() + 0.05f);
    }
}
