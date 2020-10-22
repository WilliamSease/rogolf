using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rake : Item
{
    public Rake() {
        this.name = "Rake";
        this.description = "Increase bunker friction";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        TerrainType bunker = terrainAttributes.GetBunkerTerrain();
        bunker.SetFriction(bunker.GetFriction() * 5/4f);
    }
}
