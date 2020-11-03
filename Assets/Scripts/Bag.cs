using Clubs;
using ShotModeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bag
{
    private static readonly float[] DISTANCES =   {275f,243f,230f,212f,203f,194f,183f,172f,160f,148f,136f,120f,100f,10f};
    private static readonly float[] MAX_HEIGHTS = { 32f, 30f, 31f, 27f, 28f, 31f, 30f, 32f, 31f, 30f, 29f, 30f, 30f,0.001f};

    private Game game;

    private List<Club> bagList;
    private int current;

    public Bag(Game game)
    {
        this.game = game;
        this.bagList = new List<Club>();

        // Add default clubs
        // name, power, shot loft (radians)
        this.bagList.Add(new Club(game, ClubType.ONE_WOOD,       401f, 0.010f));
        this.bagList.Add(new Club(game, ClubType.THREE_WOOD,     300f, 0.011f));
        this.bagList.Add(new Club(game, ClubType.FIVE_WOOD,      254f, 0.014f));
        this.bagList.Add(new Club(game, ClubType.THREE_IRON,     230f, 0.012f));
        this.bagList.Add(new Club(game, ClubType.FOUR_IRON,      200f, 0.014f));
        this.bagList.Add(new Club(game, ClubType.FIVE_IRON,      170f, 0.018f));
        this.bagList.Add(new Club(game, ClubType.SIX_IRON,       150f, 0.018f));
        this.bagList.Add(new Club(game, ClubType.SEVEN_IRON,     132f, 0.022f));
        this.bagList.Add(new Club(game, ClubType.EIGHT_IRON,     116f, 0.022f));
        this.bagList.Add(new Club(game, ClubType.NINE_IRON,      102f, 0.024f));
        this.bagList.Add(new Club(game, ClubType.PITCHING_WEDGE,  90f, 0.026f));
        this.bagList.Add(new Club(game, ClubType.SAND_WEDGE,      72f, 0.032f));
        this.bagList.Add(new Club(game, ClubType.LOB_WEDGE,       56f, 0.040f));
        this.bagList.Add(new Club(game, ClubType.PUTTER,          30f, 1.0E-8f));
        
        this.current = 0;
    }

    public void UpdateDistances()
    {
        foreach (Club club in bagList)
        {
            game.GetBall().SimulateDistances(club);
        }
    }

    /// <summary>
    /// Brute-force club parameters given desired output behavior.
    /// Sends results to .csv files in the root project directory.
    /// </summary>
    public void GenerateClubs()
    {
        int iterations = 1000;
        for (int i = 0; i < bagList.Count; i++)
        {
            game.GetBall().FindTrajectory(bagList[i], MathUtil.ToMeters(DISTANCES[i]), MathUtil.ToMeters(MAX_HEIGHTS[i]), iterations, i);
        }
    }

    public void SelectBestClub()
    {
        // Set to putter if on green
        if (game.GetBall().OnGreen())
        {
            current = GetPutterIndex();
            return;
        }

        float distanceToHole = MathUtil.MapDistance(game.GetBall().GetPosition(), game.GetHoleInfo().GetHolePosition());
        for (int i = GetPutterIndex() - 1; i >= 0; i--)
        {
            if (distanceToHole < bagList[i].GetDistance(Mode.NORMAL))
            {
                current = i;
                return;
            }
        }
        // Set to driver if further than other clubs
        current = 0;
    }

    public void IncrementBag()
    {
        current++;
        if (current >= bagList.Count)
        {
            current = 0;
        }
        game.GetShotMode().Validate();
    }

    public void DecrementBag()
    {
        current--;
        if (current < 0)
        {
            current = bagList.Count - 1;
        }
        game.GetShotMode().Validate();
    }

    public Club GetClub() { return bagList[current]; }
    private int GetPutterIndex() { return bagList.Count - 1; }

    public List<Club> GetRandomClubs(int n) {
        // Generate random, unique list of n indices.
        // Do not include the final index: the putter.
        List<int> indexList = Enumerable.Range(0, bagList.Count - 1).OrderBy(x => Guid.NewGuid()).Take(n).ToList();

        // Index into bag list using generated indices.
        return bagList.Where((item, index) => indexList.Contains(index)).ToList();
    }

    public void RemoveClub(Club i) { bagList.Remove(i); }
}
