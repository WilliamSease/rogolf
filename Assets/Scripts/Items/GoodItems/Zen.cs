using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Zen : Item
{
    public Zen() {
        this.name = "Zen";
        this.description = "Control/Impact up - Power down";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreaseControl(0.1f);
        playerAttributes.IncreaseImpact(0.1f);
        playerAttributes.IncreasePower(-0.05f);
    }
}
