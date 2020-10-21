using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtil
{
    public static Vector3 Vector3NaN = new Vector3(Single.NaN, Single.NaN, Single.NaN);

    public static Vector3 FromPolar(float r, float theta)
    {
        return new Vector3(Mathf.Cos(theta) * r, 0, Mathf.Sin(theta) * r);
    }
    
    public static Vector3 Rotate(Vector3 v, float theta)
    {
        float x = v.x;
        v.x = x * Mathf.Cos(theta) - v.z * Mathf.Sin(theta);
        v.z = x * Mathf.Sin(theta) + v.z * Mathf.Cos(theta);
        return v;
    }

    /// <returns>
    /// (angle between terrain and flat in direction of ballAngle in degrees, 
    ///         angle between terrain and flat in direction to the right of ballAngle in degrees)
    /// </returns>
    public static Tuple<float, float> GetTerrainAngle(Vector3 terrainNormal, float ballAngle)
    {
        // Get 'forward' vector in ball direction
        Vector3 forward = FromPolar(1f, ballAngle);
        forward.Normalize();

        // Get 'right' vector in ball direction
        Vector3 right = Rotate(FromPolar(1f, ballAngle), Mathf.PI / 2);
        right.Normalize();

        // Find angle between them
        float forwardAngle = Vector3.Angle(forward, terrainNormal) - 90f;
        float sideAngle = Vector3.Angle(right, terrainNormal) - 90f;
        return new Tuple<float, float>(forwardAngle, sideAngle);
    }

    public static Vector3 GetGroundVector(Vector3 terrainNormal, Vector3 velocity)
    {
        Vector3 groundVector = Vector3.ProjectOnPlane(velocity, terrainNormal);
        groundVector.Normalize();
        return groundVector;
    }

    /// <returns>
    /// angle between terrain and ball velocity in direction of velocity
    /// </returns>
    public static float GetVelocityAngle(Vector3 terrainNormal, Vector3 velocity)
    {
        return Vector3.Angle(velocity, GetGroundVector(terrainNormal, velocity));
    }

    public static string Vector3ToString(Vector3 v) { return String.Format("{0},{1},{2}", v.x, v.y, v.z); }
    
    public static float MapDistance(Vector3 u, Vector3 v)
    {
        return Vector3.Distance(new Vector3(u.x,0,u.z), new Vector3(v.x,0,v.z));
    }

    public static Vector3 Copy(Vector3 v) { return new Vector3(v.x, v.y, v.z); }

    public static float ToYards(float m) { return m * 1.09361f; }
    public static float ToYardsRounded(float m) { return Mathf.Round(ToYards(m)); }
    public static float ToMeters(float y) { return y * 0.9144f; }
	public static float RadsToDeg(float r) { return r * 57.2958f; }
}
