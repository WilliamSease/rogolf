using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaterTerrain : TerrainType
{
    private static float FRICTION = 0.1f;
    private static float BOUNCE = 0f;
    private static float LIE_RATE = 0.20f;
    private static float LIE_RANGE = 0.10f;

    public WaterTerrain() : base(FRICTION, BOUNCE, LIE_RATE, LIE_RANGE) { }
}
