using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MaterialTypeEnum
{
    public enum MaterialType { GREEN, FAIRWAY, ROUGH, BUNKER, WATER, NONE }
}

[System.Serializable]
public class TerrainAttributes
{
    public const float SIMULATED_FRICTION = 0.50f;
    public const float SIMULATED_BOUNCE = 0.25f;

    private TerrainType tee;
    private TerrainType green;
    private TerrainType fairway;
    private TerrainType rough;
    private TerrainType bunker;
    private TerrainType water;

    private Dictionary<MaterialType, MaterialType> swapMap;
    private Dictionary<MaterialType, TerrainType> terrainMap;

    public TerrainAttributes() {
        //                     friction,bounce,lieRate,lieRange
        tee = new TerrainType(    0.45f, 0.25f, 0.99f, 0.01f);
        green = new TerrainType(  0.45f, 0.25f, 0.99f, 0.02f);
        fairway = new TerrainType(0.50f, 0.25f, 0.99f, 0.02f);
        rough = new TerrainType(  0.60f, 0.20f, 0.80f, 0.16f);
        bunker = new TerrainType( 0.75f, 0.10f, 0.70f, 0.20f);
        water = new TerrainType(  0.99f, 0.00f, 0.20f, 0.10f); 

        ResetSwapMap();

        // Initialize terrain map
        terrainMap = new Dictionary<MaterialType, TerrainType>();
        terrainMap.Add(MaterialType.GREEN, green);
        terrainMap.Add(MaterialType.FAIRWAY, fairway);
        terrainMap.Add(MaterialType.ROUGH, rough);
        terrainMap.Add(MaterialType.BUNKER, bunker);
        terrainMap.Add(MaterialType.WATER, water);
    }

    /// <summary>
    /// Gets the bounce of a surface given a RaycastHit.
    /// </summary>
    public float GetBounce(RaycastHit terrainHit)
    {
        return GetTerrainType(terrainHit).GetBounce();
    }

    /// <summary>
    /// Gets the friction of a surface given a RaycastHit.
    /// </summary>
    public float GetFriction(RaycastHit terrainHit)
    {
        return GetTerrainType(terrainHit).GetFriction();
    }

    /// <summary>
    /// Gets the MaterialType of a surface given a string name.
    /// </summary>
    public MaterialType GetMaterialType(string name)
    {
        switch (name[0])
        {
            case 'B':
                return GetSwap(MaterialType.BUNKER);
            case 'F':
                return GetSwap(MaterialType.FAIRWAY);
            case 'G':
                return GetSwap(MaterialType.GREEN);
            case 'R':
                return GetSwap(MaterialType.ROUGH);
            case 'W':
                return GetSwap(MaterialType.WATER);
            case 'C':
                return GetSwap(MaterialType.GREEN);
            default:
                throw new Exception("Cannot not get TerrainType for name " + name);
        }
    }

    /// <summary>
    /// Gets the TerrainType of a surface given a string name.
    /// </summary>
    public TerrainType GetTerrainType(string name) { return terrainMap[GetMaterialType(name)]; }

    /// <summary>
    /// Gets the MaterialType of a surface given a RaycastHit.
    /// </summary>
    public MaterialType GetMaterialType(RaycastHit terrainHit) { return GetMaterialType(terrainHit.transform.gameObject.name); }

    /// <summary>
    /// Gets the TerrainType of a surface given a RaycastHit.
    /// </summary>
    public TerrainType GetTerrainType(RaycastHit terrainHit) { return GetTerrainType(terrainHit.transform.gameObject.name); }

    public bool OnGreen(RaycastHit terrainHit) { return terrainHit.transform.gameObject.name[0] == 'G'; }
    public bool InWater(RaycastHit terrainHit) { return terrainHit.transform.gameObject.name[0] == 'W'; }

    public TerrainType GetTeeTerrain() { return tee; }
    public TerrainType GetGreenTerrain() { return green; }
    public TerrainType GetFairwayTerrain() { return fairway; }
    public TerrainType GetRoughTerrain() { return rough; }
    public TerrainType GetBunkerTerrain() { return bunker; }
    public TerrainType GetWaterTerrain() { return water; }

    public void ResetSwapMap()
    {
        // Initialize swap map
        swapMap = new Dictionary<MaterialType, MaterialType>();
        swapMap.Add(MaterialType.GREEN, MaterialType.GREEN);
        swapMap.Add(MaterialType.FAIRWAY, MaterialType.FAIRWAY);
        swapMap.Add(MaterialType.ROUGH, MaterialType.ROUGH);
        swapMap.Add(MaterialType.BUNKER, MaterialType.BUNKER);
        swapMap.Add(MaterialType.WATER, MaterialType.WATER);
    }

    public void RandomizeSwapMap()
    {
        ResetSwapMap();
        List<MaterialType> shuffled = swapMap.Keys.OrderBy(a => Guid.NewGuid()).ToList();
        Dictionary<MaterialType, MaterialType> copy = new Dictionary<MaterialType, MaterialType>(swapMap);
        int i = 0;
        foreach (KeyValuePair<MaterialType, MaterialType> entry in copy)
        {
            swapMap[entry.Key] = shuffled[i];
            i++;
        }

        // Don't randomize green
        swapMap[MaterialType.GREEN] = MaterialType.GREEN;
    }

    public MaterialType GetSwap(MaterialType materialType) { return swapMap[materialType]; }

    public void SetSwap(MaterialType keyType, MaterialType valueType)
    {
        Dictionary<MaterialType, MaterialType> copy = new Dictionary<MaterialType, MaterialType>(swapMap);
        foreach (KeyValuePair<MaterialType, MaterialType> entry in copy)
        {
            if (entry.Value == keyType) swapMap[entry.Key] = valueType;
            else if (entry.Value == valueType) swapMap[entry.Key] = keyType;
        }
    }

    public void SetMaterial(MaterialType keyType, MaterialType valueType)
    {
        Dictionary<MaterialType, MaterialType> copy = new Dictionary<MaterialType, MaterialType>(swapMap);
        foreach (KeyValuePair<MaterialType, MaterialType> entry in copy)
        {
            if (entry.Value == keyType) swapMap[entry.Key] = valueType;
        }
    }
}
