using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpinUp : Item
{
    public SpinUp() {
        this.name = "SpinUp";
        this.description = "Spin: +10";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreaseSpin(10.0);
    }
}
