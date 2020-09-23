﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorUtil
{
    public static Vector3 NaN = new Vector3(Single.NaN, Single.NaN, Single.NaN);

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

    public static float MapDistance(Vector3 u, Vector3 v)
    {
        return Vector3.Distance(new Vector3(u.x,0,u.z), new Vector3(v.x,0,v.z));
    }
}