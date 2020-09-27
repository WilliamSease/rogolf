using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtil
{
    public static float ToYards(float m) { return m * 1.09361f; }
    public static float ToYardsRounded(float m) { return Mathf.Round(ToYards(m)); }
    public static float ToMeters(float y) { return y * 0.9144f; }
}
