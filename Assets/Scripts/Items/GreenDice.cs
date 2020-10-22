using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GreenDice : Item
{
    public GreenDice() {
        this.name = "GreenDice";
        this.description = "Adjust terrain lie attributes";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        RandomizeTerrainType(terrainAttributes.GetTeeTerrain());
        RandomizeTerrainType(terrainAttributes.GetGreenTerrain());
        RandomizeTerrainType(terrainAttributes.GetFairwayTerrain());
        RandomizeTerrainType(terrainAttributes.GetRoughTerrain());
        RandomizeTerrainType(terrainAttributes.GetBunkerTerrain());
        RandomizeTerrainType(terrainAttributes.GetWaterTerrain());
    }

    private void RandomizeTerrainType(TerrainType tt)
    {
        tt.SetLieRate(tt.GetLieRate() + UnityEngine.Random.Range(-0.25f, 0.25f));
        tt.SetLieRange(tt.GetLieRange() + UnityEngine.Random.Range(-0.10f, 0.20f));
    }
}
