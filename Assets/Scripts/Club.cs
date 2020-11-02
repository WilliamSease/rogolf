using Clubs;
using ShotModeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club
{
    private Game game;

    private ClubType type;
    private float power;
    private float loft;
    private Dictionary<Mode, float> distances;

    public Club(Game game, ClubType type, float power, float loft)
    {
        this.game = game;
        this.type = type;
        this.power = power;
        this.loft = loft;
        // Distances get simulated by the bag
        this.distances = new Dictionary<Mode, float>();
    }

    public ClubType GetClubType() { return type; }
    public ClubClass GetClubClass() { return type.GetClubClass(); }
    public string GetName() { return type.GetClubName(); }
    public float GetPower() { return power; }
    public float GetLoft() { return loft; }

    public float GetDistance(Mode mode) { return distances[mode]; }
    public float GetDistance() { return GetDistance(game.GetShotMode().GetShotMode()); }

    public void SetPower(float power) { this.power = power; }
    public void SetLoft(float loft) { this.loft = loft; }
    public void SetDistance(Mode mode, float distance) { distances.Add(mode, distance); }
}
