using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SharpBlades : Item
{
    public SharpBlades() {
        this.name = "SharpBlades";
        this.description = "Improve rough lies";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        TerrainType rough = terrainAttributes.GetRoughTerrain();
        rough.SetLieRate(rough.GetLieRate() + 0.10f);
        rough.SetLieRange(rough.GetLieRange() * 0.5f);
    }
}
