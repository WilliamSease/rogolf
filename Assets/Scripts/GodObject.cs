using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodObject
{
    public const string NAME = "GodObject";

    public static GameObject Create()
    {
        return new GameObject(NAME);
    }
}
