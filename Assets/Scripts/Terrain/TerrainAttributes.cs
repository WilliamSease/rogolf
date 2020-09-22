using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainAttributes
{
    private TerrainType tee;
    private TerrainType green;
    private TerrainType fairway;
    private TerrainType rough;
    private TerrainType bunker;
    private TerrainType water;

    public TerrainAttributes() {
        tee = new TeeTerrain();
        green = new GreenTerrain();
        fairway = new FairwayTerrain();
        rough = new RoughTerrain();
        bunker = new BunkerTerrain();
        water = new WaterTerrain();
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
    /// Gets the TerrainType of a surface given a RaycastHit.
    /// </summary>
    private TerrainType GetTerrainType(RaycastHit terrainHit)
    {
        string gameObjectName = terrainHit.transform.gameObject.name;
        switch (gameObjectName[0])
        {
            case 'B':
                return bunker;
                break;
            case 'F':
                return fairway;
                break;
            case 'G':
                return green;
                break;
            case 'R':
                return rough;
                break;
            case 'W':
                return water;
                break;
            default:
                throw new InvalidOperationException("Cannot not get TerrainType for GameObject " + gameObjectName);
                break;
        }
    }

    public TerrainType GetTeeTerain() { return tee; }
    public TerrainType GetGreenTerain() {return green; }
    public TerrainType GetFairwayTerain() { return fairway; }
    public TerrainType GetRoughTerain() { return rough; }
    public TerrainType GetBunkerTerain() { return bunker; }
    public TerrainType GetWaterTerain() { return water; }

}
