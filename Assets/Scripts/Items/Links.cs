using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Links : Item
{
    public Links() {
        this.name = "Links";
        this.description = "Increase rough friction";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        TerrainType rough = terrainAttributes.GetRoughTerrain();
        rough.SetFriction(rough.GetFriction() * 5/4f);
    }
}
