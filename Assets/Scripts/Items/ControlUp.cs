using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControlUp : Item
{
    public ControlUp() {
        this.name = "ControlUp";
        this.description = "Control: +10";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreaseControl(10.0);
    }
}
