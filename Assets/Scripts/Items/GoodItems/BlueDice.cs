using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueDice : Item
{
    public BlueDice() {
        this.name = "BlueDice";
        this.description = "Adjust player stats";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.IncreasePower(UnityEngine.Random.Range(-0.5f, 0.5f));
        playerAttributes.IncreaseControl(UnityEngine.Random.Range(-0.5f, 0.5f));
        playerAttributes.IncreaseImpact(UnityEngine.Random.Range(-0.5f, 0.5f));
        playerAttributes.IncreaseSpin(UnityEngine.Random.Range(-0.5f, 0.5f));
    }
}
