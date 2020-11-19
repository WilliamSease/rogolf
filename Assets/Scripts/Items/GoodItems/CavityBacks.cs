using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CavityBacks : Item
{
    public CavityBacks() {
        this.name = "CavityBacks";
        this.description = "Control/Impact up - Spin down";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreaseControl(0.1f);
        playerAttributes.IncreaseImpact(0.1f);
        playerAttributes.IncreaseSpin(-0.05f);
    }
}
