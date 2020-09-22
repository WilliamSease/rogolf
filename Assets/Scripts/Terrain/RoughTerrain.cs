using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoughTerrain : TerrainType
{
    private static float FRICTION = 0.90f;
    private static float BOUNCE = 0.3f;
    private static float LIE_RATE = 0.80f;
    private static float LIE_RANGE = 0.16f;

    public RoughTerrain() : base(FRICTION, BOUNCE, LIE_RATE, LIE_RANGE) { }
}
