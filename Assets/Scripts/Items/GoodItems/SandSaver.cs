using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SandSaver : Item
{
    public SandSaver() {
        this.name = "SandSaver";
        this.description = "Improve bunker lies";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        TerrainType bunker = terrainAttributes.GetBunkerTerrain();
        bunker.SetLieRate(bunker.GetLieRate() + 0.10f);
        bunker.SetLieRange(bunker.GetLieRange() * 0.5f);
    }
}
