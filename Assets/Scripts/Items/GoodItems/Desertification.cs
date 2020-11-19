using MaterialTypeEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Desertification : Item
{
    public Desertification() {
        this.name = "Desertification";
        this.description = "Rough becomes sand";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        terrainAttributes.SetMaterial(MaterialType.ROUGH, MaterialType.BUNKER);
    }
}
