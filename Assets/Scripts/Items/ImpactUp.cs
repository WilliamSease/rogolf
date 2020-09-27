using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImpactUp : Item
{
    public ImpactUp() {
        this.name = "ImpactUp";
        this.description = "Impact: +10";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreaseImpact(10.0);
    }
}
