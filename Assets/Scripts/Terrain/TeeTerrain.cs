using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeeTerrain : TerrainType
{
    public static float FRICTION = 0.95f;
    public static float BOUNCE = 0.3f;
    private static float LIE_RATE = 0.99f;
    private static float LIE_RANGE = 0.02f;

    public TeeTerrain() : base(FRICTION, BOUNCE, LIE_RATE, LIE_RANGE) { }
}
