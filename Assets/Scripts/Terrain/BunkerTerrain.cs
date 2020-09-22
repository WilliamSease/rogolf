using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BunkerTerrain : TerrainType
{
    private static float FRICTION = 0.5f;
    private static float BOUNCE = 0.1f;
    private static float LIE_RATE = 0.70f;
    private static float LIE_RANGE = 0.20f;

    public BunkerTerrain() : base(FRICTION, BOUNCE, LIE_RATE, LIE_RANGE) { }
}
