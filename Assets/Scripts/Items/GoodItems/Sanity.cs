using MaterialTypeEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sanity : Item
{
    public Sanity() {
        this.name = "Sanity";
        this.description = "Reset terrain swaps";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        terrainAttributes.ResetSwapMap();
    }
}
