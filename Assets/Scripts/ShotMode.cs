using Clubs;
using ShotModeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotModeEnum
{
    public enum Mode { NORMAL, POWER, APPROACH }
}

public class ShotMode
{
    private const int DEFAULT_POWER_SHOTS = 2;

    private Game game;

    private Mode mode;
    private int powerShots;

    public ShotMode(Game game)
    {
        this.game = game;
        powerShots = 0;
        Reset();
    }

    public void Reset()
    {
        mode = Mode.NORMAL;
    }

    /// <summary>
    /// Toggle shot mode based on current mode and club.
    /// </summary>
    public void Toggle()
    {
        ClubType clubType = game.GetBag().GetClub().GetClubType();
        if (mode == Mode.NORMAL)
        {
            mode = Mode.POWER;
        }
        else if (mode == Mode.POWER)
        {
            if (clubType.GetClubClass() == ClubClass.WEDGE) mode = Mode.APPROACH;
            else mode = Mode.NORMAL;
        }
        else if (mode == Mode.APPROACH)
        {
            mode = Mode.NORMAL;
        }
        else throw new Exception("Unexpected shot Mode");
    }

    /// <summary>
    /// Validate that shot mode and club combo is valid. Correct otherwise.
    /// </summary>
    public void Validate()
    {
        ClubType clubType = game.GetBag().GetClub().GetClubType();
        if (mode == Mode.APPROACH && clubType.GetClubClass() != ClubClass.WEDGE)
        {
            mode = Mode.NORMAL;
        }
    }

    public void SetPowerShots(int powerShots) { this.powerShots = powerShots; }
    public void SetPowerShots() { powerShots = DEFAULT_POWER_SHOTS; }

    public Mode GetShotMode() { return mode; }
}
