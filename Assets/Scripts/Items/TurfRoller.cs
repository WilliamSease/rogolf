using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurfRoller : Item
{
    public TurfRoller() {
        this.name = "TurfRoller";
        this.description = "Decrease green friction";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        TerrainType green = terrainAttributes.GetGreenTerrain();
        green.SetFriction(green.GetFriction() * 4/5f);
    }
}
