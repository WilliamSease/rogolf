using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private List<ShotConditional> conditionals;

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

        conditionals = new List<ShotConditional>();
        conditionals.Add(new Fir());
        conditionals.Add(new UnderGir());
        conditionals.Add(new Gir());
        conditionals.Add(new Bunker());
        conditionals.Add(new Water());
        // TODO
        // Check approach
        // Check long drive
        // ...

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

    public List<Tuple<string, int>> AddShotScore()
    {
        List<Tuple<string, int>> output = new List<Tuple<string, int>>();

        int strokes = game.GetHoleBag().GetCurrentHoleData().GetStrokes();
        int par = game.GetHoleInfo().GetPar();
        MaterialType terrain = game.GetBall().GetMaterialType();

        foreach (ShotConditional conditional in conditionals)
        {
            output.Add(conditional.Execute(par, strokes, terrain, game));
        }

        return (from item in output where item != null select item).ToList();
    }

    /// <summary>
    /// Earn score. Returns score earned.
    /// </summary>
    public int AddCredit(int credit)
    {
        this.credit += credit;
        return credit;
    }

    /// <summary>
    /// Spend score. Returns score spent.
    /// </summary>
    public int AddDebit(int debit)
    {
        this.debit += debit;
        return debit;
    }

    /// <summary>
    /// Get the total score ('credits') earned. 
    /// </summary>
    public int GetEarnings() { return credit; }

    /// <summary>
    /// Get the current score ('credits') count; score earned minus score spent. 
    /// </summary>
    public int GetCredits() { return credit - debit; }
}
