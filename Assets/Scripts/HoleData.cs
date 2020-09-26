using System;
using System.Collections;
using System.Collections.Generic;
using TeeEnum;
using UnityEngine;

[System.Serializable]
public class HoleData
{
    private Tee tee;
    private float lengthFront;
    private float lengthBack;
    private int par;

    private int strokes;
    private int putts;
    private bool fir;
    private bool gir;

    public HoleData(string name, Tee tee, float lengthFront, float lengthBack, int par)
    {
        this.tee = tee;
        this.lengthFront = lengthFront;
        this.lengthBack = lengthBack;
        this.par = par;

        strokes = 0;
        putts = 0;
        fir = false;
        gir = false;
    }

    public void IncrementStrokes() { strokes++; }
    public void IncrementPutts() { putts++; }
    public void ApplyFir(bool fir) { this.fir = this.fir ? true : fir; }
    public void ApplyGir(bool gir) { this.gir = this.gir ? true : gir; }

    public void SetStrokes(int strokes) { this.strokes = strokes; }
    public void SetPutts(int putts) { this.putts = putts; }
    public void SetFir(bool fir) { this.fir = fir; }
    public void SetGir(bool gir) { this.gir = gir; }

    public Tee GetTee() { return tee; }
    public float GetLengthFront() { return lengthFront; }
    public float GetLengthBack() { return lengthBack; }
    public int GetPar() { return par; }
    public int GetStrokes() { return strokes; }
    public int GetPutts() { return putts; }
    public bool GetFir() { return fir; }
    public bool GetGir() { return gir; }
}
