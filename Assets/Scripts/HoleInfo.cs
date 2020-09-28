using System;
using System.Collections;
using System.Collections.Generic;
using TeeEnum;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeeEnum
{
    [System.Serializable]
    public enum Tee { FRONT, BACK }
}

public class HoleInfo
{
    private const float PAR_3_MAX_LENGTH = 228.6f;
    private const float PAR_4_MAX_LENGTH = 429.8f;
    private const float PAR_5_MAX_LENGTH = 630.9f;

    private Game game;

    private int par;
    private Tee tee;
    private Vector3 frontTeePosition;
    private Vector3 backTeePosition;
    private Vector3 holePosition;

    private float lengthFront;
    private float lengthBack;

    public HoleInfo(Game game, Tee tee, Vector3 frontTeePosition, Vector3 backTeePosition, Vector3 holePosition, int par)
    {
        this.tee = tee;
        this.frontTeePosition = frontTeePosition;
        this.backTeePosition = backTeePosition;
        this.holePosition = holePosition;

        CalculateHoleLength();
        this.par = par;
    }

    public HoleInfo(Game game, Tee tee, Vector3 frontTeePosition, Vector3 backTeePosition, Vector3 holePosition)
    {
        this.game = game;
        this.tee = tee;
        this.frontTeePosition = frontTeePosition;
        this.backTeePosition = backTeePosition;
        this.holePosition = holePosition;

        CalculateHoleLength();
        CalculatePar();
    }

    public void AddCurrentHole()
    {
        game.GetHoleBag().AddHole(new HoleData(SceneManager.GetActiveScene().name, tee, lengthFront, lengthBack, par));
    }

    public void CalculateHoleLength()
    {
        // Calculate hole length 'as the crow flies'
        Vector3 frontTeePositionNoHeight = new Vector3(frontTeePosition.x, 0, frontTeePosition.z);
        Vector3 backTeePositionNoHeight = new Vector3(backTeePosition.x, 0, backTeePosition.z);
        Vector3 holePositionNoHeight = new Vector3(holePosition.x, 0, holePosition.z);
        this.lengthFront = Vector3.Distance(frontTeePositionNoHeight, holePositionNoHeight);
        this.lengthBack = Vector3.Distance(backTeePositionNoHeight, holePositionNoHeight);
    }

    public void CalculatePar()
    {
        float length = GetLength();
        if (length < PAR_3_MAX_LENGTH) { par = 3; }
        else if (length < PAR_4_MAX_LENGTH) { par = 4; }
        else if (length < PAR_5_MAX_LENGTH) { par = 5; }
        else { par = 6; }
    }

    public int GetPar() { return par; }
    public Tee GetTee() { return tee; }
    public Vector3 GetFrontTeePosition() { return frontTeePosition; }
    public Vector3 GetBackTeePosition() { return backTeePosition; }
    public Vector3 GetHolePosition() { return holePosition; }

    public float GetLengthFront() { return lengthFront; }
    public float GetLengthBack() { return lengthBack; }

    public Vector3 GetTeePosition()
    {
        // TODO - remove this added height? Still working around it
        if (tee == Tee.FRONT) { return new Vector3(frontTeePosition.x, frontTeePosition.y+0.1f, frontTeePosition.z); }
        else { return backTeePosition; }
    }

    public float GetLength() {
        if (tee == Tee.FRONT) return lengthFront;
        else return lengthBack;
    }

    public override string ToString()
    {
        return String.Format("HoleInfo (click for more info)\nP : {0}\nT : {1}\nFP: {2}\nBP: {3}\nHP: {4}\nFL: {5}\nBL: {6}\n", 
            par, tee, frontTeePosition, backTeePosition, holePosition, lengthFront, lengthBack);
    }
}
