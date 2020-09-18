using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorUtil
{
    public static Vector3 FromPolar(float r, float theta)
    {
        return new Vector3(Mathf.Cos(theta) * r, 0, Mathf.Sin(theta) * r);
    }
    
    public static void Rotate(Vector3 v, float theta)
    {
        float x = v.x;
        v.x = x * Mathf.Cos(theta) - v.z * Mathf.Sin(theta);
        v.z = x * Mathf.Sin(theta) + v.z * Mathf.Cos(theta);
    }
}
