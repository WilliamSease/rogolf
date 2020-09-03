using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BobBehavior : MonoBehaviour
{
    public GameObject tgt;
    public GameObject prnt;
    double pos = 0;

    // Start is called before the first frame update
    void Start()
    {
        tgt.transform.parent = prnt.transform;
    }

    // Update is called once per frame
    void Update()
    {
        prnt.transform.Rotate(new Vector3(0.0f, .1f, 0f));
        prnt.transform.Translate(new Vector3(0.0f, ((float)Math.Cos(pos))/1200f, 0f));
        pos += .001;
    }
}
