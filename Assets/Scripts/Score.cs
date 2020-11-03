using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    private const int ACE = 500;
    private const int ALBATROSS = 400;
    private const int EAGLE = 300;
    private const int BIRDIE = 200;
    private const int PAR = 100;
    private const int BOGEY = 0;
    private const int OTHER = 0;

    private Game game;

    /// <summary>
    /// Total score earned.
    /// </summary>
    private int credit;

    /// <summary>
    /// Total score spent.
    /// </summary>
    private int debit;

    public Score(Game game)
    {
        this.game = game;

        credit = 0;
        debit = 0;
    }

    public int AddHoleScore()
    {
        int holeScore = 0;
        int strokes = game.GetHoleBag().GetCurrentHoleData().GetStrokes();
        int par = game.GetHoleInfo().GetPar();

        if (strokes <= 1)
        {
            holeScore = ACE;
        }
        else
        {
            switch (strokes - par)
            {
                case -3: holeScore = ALBATROSS; break;
                case -2: holeScore = EAGLE; break;
                case -1: holeScore = BIRDIE; break;
                case  0: holeScore = PAR; break;
                case  1: holeScore = BOGEY; break;
                default: holeScore = OTHER; break;
            }
        }

        AddCredit(holeScore);
        return holeScore;
    }

    public int AddShotScore()
    {
        throw new Exception("pls implement");
    }

    /// <summary>
    /// Earn score.
    /// </summary>
    public void AddCredit(int credit) { this.credit += credit; }

    /// <summary>
    /// Spend score.
    /// </summary>
    public void AddDebit(int debit) { this.debit += debit; }

    /// <summary>
    /// Get the total score ('credits') earned. 
    /// </summary>
    public int GetEarnings() { return credit; }

    /// <summary>
    /// Get the current score ('credits') count; score earned minus score spent. 
    /// </summary>
    public int GetCredits() { return credit - debit; }
}
