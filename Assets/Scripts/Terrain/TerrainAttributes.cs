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

    public TerrainType GetTeeTerain() { return tee; }
    public TerrainType GetGreenTerain() {return green; }
    public TerrainType GetFairwayTerain() { return fairway; }
    public TerrainType GetRoughTerain() { return rough; }
    public TerrainType GetBunkerTerain() { return bunker; }
    public TerrainType GetWaterTerain() { return water; }

}
