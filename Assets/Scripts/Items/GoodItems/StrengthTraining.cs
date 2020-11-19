using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StrengthTraining : Item
{
    public StrengthTraining() {
        this.name = "StrengthTraining";
        this.description = "Power up - Impact down";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreasePower(0.15f);
        playerAttributes.IncreaseImpact(-0.05f);
    }
}
