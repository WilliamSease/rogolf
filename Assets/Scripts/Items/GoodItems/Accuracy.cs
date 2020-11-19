using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Accuracy : Item
{
    public Accuracy() {
        this.name = "Accuracy";
        this.description = "Impact up - Control down";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreaseImpact(0.15f);
        playerAttributes.IncreaseControl(-0.05f);
    }
}
