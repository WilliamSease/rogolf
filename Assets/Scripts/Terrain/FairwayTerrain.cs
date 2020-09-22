using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FairwayTerrain : TerrainType
{
    private static float FRICTION = 0.98f;
    private static float BOUNCE = 0.3f;
    private static float LIE_RATE = 0.99f;
    private static float LIE_RANGE = 0.02f;

    public FairwayTerrain() : base(FRICTION, BOUNCE, LIE_RATE, LIE_RANGE) { }
}
