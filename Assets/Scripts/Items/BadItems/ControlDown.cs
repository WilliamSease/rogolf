using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControlDown : Item
{
    public ControlDown() {
        this.name = "ControlDown";
        this.description = "Control: -10";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreaseControl(-0.1f);
    }
}
