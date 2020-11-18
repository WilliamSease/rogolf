using MaterialTypeEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlashFlood : Item
{
    public FlashFlood() {
        this.name = "FlashFlood";
        this.description = "Bunkers become water";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        terrainAttributes.SetMaterial(MaterialType.BUNKER, MaterialType.WATER);
    }
}
