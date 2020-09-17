using System.Collections;
using System.Collections.Generic;
using TeeEnum;
using UnityEngine;

namespace TeeEnum
{
    public enum Tee { FRONT, BACK }
}

public class HoleInfo
{
    private const float PAR_3_MAX_YARDS = 228.6f;
    private const float PAR_4_MAX_YARDS = 429.8f;
    private const float PAR_5_MAX_YARDS = 630.9f;

    private int holeNumber;
    private int par;
    private Tee tee;
    private Vector3 frontTeePosition;
    private Vector3 backTeePosition;
    private Vector3 holePosition;

    private float yardsFront;
    private float yardsBack;

    public HoleInfo(int holeNumber, Tee tee, Vector3 frontTeePosition, Vector3 backTeePosition, Vector3 holePosition, int par)
    {
        this.holeNumber = holeNumber;
        this.frontTeePosition = frontTeePosition;
        this.backTeePosition = backTeePosition;
        this.tee = tee;

        CalculateHoleYards();
        this.par = par;
    }

    public HoleInfo(int holeNumber, Tee tee, Vector3 frontTeePosition, Vector3 backTeePosition, Vector3 holePosition)
    {
        this.holeNumber = holeNumber;
        this.frontTeePosition = frontTeePosition;
        this.backTeePosition = backTeePosition;
        this.tee = tee;

        CalculateHoleYards();
        CalculatePar();
    }

    public void CalculateHoleYards()
    {
        // Calculate hole yards 'as the crow flies'
        Vector3 frontTeePositionNoHeight = new Vector3(frontTeePosition.x, 0, frontTeePosition.z);
        Vector3 backTeePositionNoHeight = new Vector3(backTeePosition.x, 0, backTeePosition.z);
        Vector3 holePositionNoHeight = new Vector3(holePosition.x, 0, holePosition.z);
        this.yardsFront = Vector3.Distance(frontTeePositionNoHeight, holePositionNoHeight);
        this.yardsBack = Vector3.Distance(backTeePositionNoHeight, holePositionNoHeight);
    }

    public void CalculatePar()
    {
        float yards = GetYards();
        if (yards < PAR_3_MAX_YARDS) { par = 3; }
        else if (yards < PAR_4_MAX_YARDS) { par = 4; }
        else if (yards < PAR_5_MAX_YARDS) { par = 5; }
        else { par = 6; }
    }

    public int GetHoleNumber() { return holeNumber; }
    public int GetPar() { return par; }
    public Tee GetTee() { return tee; }
    public Vector3 GetFrontTeePosition() { return frontTeePosition; }
    public Vector3 GetBackTeePosition() { return backTeePosition; }
    public Vector3 GetHolePosition() { return holePosition; }

    public float GetYardsFront() { return yardsFront; }
    public float GetYardsBack() { return yardsBack; }

    public Vector3 GetTeePosition()
    {
        if (tee == Tee.FRONT) { return frontTeePosition; }
        else { return backTeePosition; }
    }

    public float GetYards() {
        if (tee == Tee.FRONT) return yardsFront;
        else return yardsBack;
    }
}
