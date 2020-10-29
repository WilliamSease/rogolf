using MaterialTypeEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Drought : Item
{
    public Drought() {
        this.name = "Drought";
        this.description = "Water becomes sand";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        terrainAttributes.SetSwap(MaterialType.WATER, MaterialType.BUNKER);
    }
}

