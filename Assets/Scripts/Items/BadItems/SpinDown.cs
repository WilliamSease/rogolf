using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpinDown : Item
{
    public SpinDown() {
        this.name = "SpinDown";
        this.description = "Spin: -10";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreaseSpin(-0.1f);
    }
}
