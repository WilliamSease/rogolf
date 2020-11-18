using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImpactDown : Item
{
    public ImpactDown() {
        this.name = "ImpactDown";
        this.description = "Impact: -10";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreaseImpact(-0.1f);
    }
}
