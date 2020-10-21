using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaterialTypeEnum
{
    public enum MaterialType { GREEN, FAIRWAY, ROUGH, BUNKER, WATER }
}

[System.Serializable]
public class TerrainAttributes
{
    public const float SIMULATED_FRICTION = 2.5E-3f;
    public const float SIMULATED_BOUNCE = 0.3f;

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
        tee = new TerrainType(    2.2E-4f, 0.30f, 0.99f, 0.02f);
        green = new TerrainType(  2.2E-4f, 0.30f, 0.99f, 0.02f);
        fairway = new TerrainType(2.5E-3f, 0.30f, 0.99f, 0.02f);
        rough = new TerrainType(  1.0E-2f, 0.25f, 0.80f, 0.16f);
        bunker = new TerrainType( 2.5E-2f, 0.10f, 0.70f, 0.20f);
        water = new TerrainType(  1.0E-1f, 0.00f, 0.20f, 0.10f); 

        // Initialize swap map
        swapMap = new Dictionary<MaterialType, MaterialType>();
        swapMap.Add(MaterialType.GREEN, MaterialType.GREEN);
        swapMap.Add(MaterialType.FAIRWAY, MaterialType.FAIRWAY);
        swapMap.Add(MaterialType.ROUGH, MaterialType.ROUGH);
        swapMap.Add(MaterialType.BUNKER, MaterialType.BUNKER);
        swapMap.Add(MaterialType.WATER, MaterialType.WATER);

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
    /// Gets the TerrainType of a surface given a string name.
    /// </summary>
    public TerrainType GetTerrainType(string name)
    {
        switch (name[0])
        {
            case 'B':
                return terrainMap[GetSwap(MaterialType.BUNKER)];
            case 'F':
                return terrainMap[GetSwap(MaterialType.FAIRWAY)];
            case 'G':
                return terrainMap[GetSwap(MaterialType.GREEN)];
            case 'R':
                return terrainMap[GetSwap(MaterialType.ROUGH)];
            case 'W':
                return terrainMap[GetSwap(MaterialType.WATER)];
            default:
                throw new InvalidOperationException("Cannot not get TerrainType for name " + name);
        }
    }

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

    public MaterialType GetSwap(MaterialType materialType) { return swapMap[materialType]; }
    public void SetSwap(MaterialType keyType, MaterialType valueType) { swapMap[keyType] = valueType; }
}
