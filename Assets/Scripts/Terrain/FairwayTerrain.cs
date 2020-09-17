using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FairwayTerrain : TerrainType
{
    private static double FRICTION = 0.98;
    private static double BOUNCE = 0.3;
    private static double LIE_RATE = 0.99;
    private static double LIE_RANGE = 0.02;

    public FairwayTerrain() : base(FRICTION, BOUNCE, LIE_RATE, LIE_RANGE) { }
}
