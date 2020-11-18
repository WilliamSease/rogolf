using MaterialTypeEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Insanity : Item
{
    public Insanity() {
        this.name = "Insanity";
        this.description = "Randomize terrain swaps";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        terrainAttributes.RandomizeSwapMap();
    }
}
