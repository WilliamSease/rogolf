using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerUp : Item
{
    public PowerUp() {
        this.name = "PowerUp";
        this.description = "Power: +10";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreasePower(0.1f);
    }
}
