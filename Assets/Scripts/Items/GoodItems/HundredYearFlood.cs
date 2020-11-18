using MaterialTypeEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HundredYearFlood : Item
{
    public HundredYearFlood() {
        this.name = "HundredYearFlood";
        this.description = "Rough becomes water";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        terrainAttributes.SetMaterial(MaterialType.ROUGH, MaterialType.WATER);
    }
}
