using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Blades : Item
{
    public Blades() {
        this.name = "Blades";
        this.description = "Spin up - Control/Impact down";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreaseSpin(0.2f);
        playerAttributes.IncreaseControl(-0.05f);
        playerAttributes.IncreaseImpact(-0.05f);
    }
}
