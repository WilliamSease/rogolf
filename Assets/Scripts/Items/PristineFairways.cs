using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PristineFairways : Item
{
    public PristineFairways() {
        this.name = "PristineFairways";
        this.description = "Perfect fairway lies";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        TerrainType fairway = terrainAttributes.GetFairwayTerrain();
        fairway.SetLieRate(1f);
        fairway.SetLieRange(0f);
    }
}
