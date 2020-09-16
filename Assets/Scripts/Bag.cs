using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag
{
    private Game game;

    private List<Club> bagList;
    private int current;

    public Bag(Game game)
    {
        this.game = game;
        this.bagList = new List<Club>();

        // Add default clubs
        // name, power, shot loft (radians)
        this.bagList.Add(new Club("1W", 10, 0.25));
        this.bagList.Add(new Club("5I", 10, 0.6));
        this.bagList.Add(new Club("SW", 10, 1));
        this.bagList.Add(new Club("P", 10, 0));

        this.current = 0;
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
