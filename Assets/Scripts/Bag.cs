using System;
using System.Collections;
using System.Collections.Generic;
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
        this.bagList.Add(new Club("1W", 776f, 0.047f));
        this.bagList.Add(new Club("3W", 465f, 0.054f));
        this.bagList.Add(new Club("5W", 371f, 0.059f));
        this.bagList.Add(new Club("3I", 297f, 0.059f));
        this.bagList.Add(new Club("4I", 251f, 0.065f));
        this.bagList.Add(new Club("5I", 211f, 0.077f));
        this.bagList.Add(new Club("6I", 181f, 0.081f));
        this.bagList.Add(new Club("7I", 149f, 0.095f));
        this.bagList.Add(new Club("8I", 125f, 0.101f));
        this.bagList.Add(new Club("9I", 105f, 0.109f));
        this.bagList.Add(new Club("PW", 89f, 0.119f));
        this.bagList.Add(new Club("SW", 67f, 0.147f));
        this.bagList.Add(new Club("LW", 49f, 0.185f));
        this.bagList.Add(new Club("P",  35f, 1.0E-7f));

        // Calculate distances
        foreach (Club club in bagList)
        {
            game.GetBall().SimulateDistance(club);
        }

        this.current = 0;
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
            current = bagList.Count - 1;
            return;
        }

        float distanceToHole = MathUtil.MapDistance(game.GetBall().GetPosition(), game.GetHoleInfo().GetHolePosition());
        for (int i = bagList.Count - 2; i >= 0; i--)
        {
            if (distanceToHole < bagList[i].GetDistance())
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
    }

    public void DecrementBag()
    {
        current--;
        if (current < 0)
        {
            current = bagList.Count - 1;
        }
    }

    public Club GetClub() { return bagList[current]; }
}
