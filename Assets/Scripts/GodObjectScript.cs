using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodObjectScript : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start() { }
    void Update() { }
}
