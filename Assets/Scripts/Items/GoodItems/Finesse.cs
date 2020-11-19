using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Finesse : Item
{
    public Finesse() {
        this.name = "Finesse";
        this.description = "Control/Spin up - Power down";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreaseControl(0.1f);
        playerAttributes.IncreaseSpin(0.1f);
        playerAttributes.IncreasePower(-0.05f);
    }
}
