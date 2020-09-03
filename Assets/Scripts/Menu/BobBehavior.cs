using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BobBehavior : MonoBehaviour
{
    public GameObject tgt;
    public GameObject prnt;

    public double perFrame; //.1f works well for this.
    public double translationMod; //1200 works well for this.
    public double translationInc; //.001 works well for this.
    double pos = 0;

    // Start is called before the first frame update
    void Start()
    {
        tgt.transform.parent = prnt.transform;
    }

    // Update is called once per frame
    void Update()
    {
        for (double i = .000; i < Time.deltaTime; i += .001)
        {
            prnt.transform.Rotate(new Vector3(0.0f, (float)perFrame, 0f));
            prnt.transform.Translate(new Vector3(0.0f, ((float)Math.Cos(pos)) / (float)translationMod, 0f));
            pos += translationInc;
        }
    }
}
