using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Precision : Item
{
    public Precision() {
        this.name = "Precision";
        this.description = "Control up - Impact down";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreaseControl(0.15f);
        playerAttributes.IncreaseImpact(-0.05f);
    }
}
