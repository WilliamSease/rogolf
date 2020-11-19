using Clubs;
using ShotModeEnum;
using SoundEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikingState : State
{
    public StrikingState(Game game) : base(game) { }

    public override void Tick()
    {
        Mode shotMode = game.GetShotMode().GetShotMode();
        // Hit the damn thing
        game.GetBall().Strike(shotMode, game.GetBag().GetClub(), game.GetPowerbar().GetPower(), game.GetPowerbar().GetAccuracy());
        game.GetShotMode().Strike();

        PlayStrikeSound();

        HoleData hole = game.GetHoleBag().GetCurrentHoleData();
        hole.IncrementStrokes();
        if (game.GetBag().IsPutter()) { hole.IncrementPutts(); }

        game.SetState(new RunningState(game));
    }

    private void PlayStrikeSound()
    {
        Sound strikeSound;
        switch (game.GetBag().GetClub().GetClubClass())
        {
            case ClubClass.WEDGE:
                strikeSound = Sound.WEDGEHIT;
                break;
            case ClubClass.PUTTER:
                strikeSound = Sound.PUTTHIT;
                break;
            default:
                strikeSound = Sound.IRONHIT;
                break;
        }
        BoomBox.Play(strikeSound);
    }
}
