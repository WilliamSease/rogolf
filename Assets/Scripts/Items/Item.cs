using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item
{
    protected string name;
    protected string description;

    public Item() { }

    public string GetName() { return name; }
    public string GetDescription() { return description; }
    public abstract void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes);
}
