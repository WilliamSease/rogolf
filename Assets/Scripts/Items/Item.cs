using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public string name;

    public Item() { }

    public abstract void Apply(PlayerAttributes playerAttributes, TerrainAttributes terrainAttributes);

}
