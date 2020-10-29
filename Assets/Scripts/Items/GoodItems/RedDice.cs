using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RedDice : Item
{
    public RedDice() {
        this.name = "RedDice";
        this.description = "Randomize player stats";
    }

    public override void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes)
    {
        playerAttributes.SetPower(UnityEngine.Random.Range(0f, 1f));
        playerAttributes.SetControl(UnityEngine.Random.Range(0f, 1f));
        playerAttributes.SetImpact(UnityEngine.Random.Range(0f, 1f));
        playerAttributes.SetSpin(UnityEngine.Random.Range(0f, 1f));
    }
}
