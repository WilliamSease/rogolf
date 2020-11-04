using System;
using System.Collections;
using System.Collections.Generic;
using TeeEnum;
using UnityEngine;

[System.Serializable]
public class HoleData
{
    private string name;
    private Tee tee;
    private float lengthFront;
    private float lengthBack;
    private int par;
    private float hcp;

    private int strokes;
    private int putts;
    private bool fir;
    private bool gir;
    private bool bunker;
    private bool water;

    public HoleData(string name, Tee tee, float lengthFront, float lengthBack, int par, float hcp)
    {
        this.name = name;
        this.tee = tee;
        this.lengthFront = lengthFront;
        this.lengthBack = lengthBack;
        this.par = par;
        this.hcp = hcp;

        strokes = 0;
        putts = 0;
        fir = false;
        gir = false;
        bunker = false;
        water = false;
    }

    public void IncrementStrokes() { strokes++; }
    public void IncrementPutts() { putts++; }
    public void ApplyFir(bool fir) { this.fir = this.fir ? true : fir; }
    public void ApplyGir(bool gir) { this.gir = this.gir ? true : gir; }

    public void SetStrokes(int strokes) { this.strokes = strokes; }
    public void SetPutts(int putts) { this.putts = putts; }
    
    public void SetFir() { this.fir = true; }
    public void SetGir() { this.gir = true; }
    public void SetBunker() { this.bunker = true; }
    public void SetWater() { this.water = true; }

    public void SetFir(bool fir) { this.fir = fir; }
    public void SetGir(bool gir) { this.gir = gir; }
    public void SetBunker(bool bunker) { this.bunker = bunker; }
    public void SetWater(bool water) { this.water = water; }

    public string GetName() { return name; }
    public Tee GetTee() { return tee; }
    public float GetLengthFront() { return lengthFront; }
    public float GetLengthBack() { return lengthBack; }
    public int GetPar() { return par; }
    public float GetHandicap() { return hcp; }
    public int GetStrokes() { return strokes; }
    public int GetPutts() { return putts; }
    public bool GetFir() { return fir; }
    public bool GetGir() { return gir; }
    public bool GetBunker() { return bunker; }
    public bool GetWater() { return water; }

    public override string ToString()
    {
        return String.Format("N:{0},T:{1},F:{2}y,B:{3}y,P:{4}", 
            name, tee, MathUtil.ToYardsRounded(lengthFront), MathUtil.ToYardsRounded(lengthBack), par);
    }
}
