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
        this.bagList.Add(new Club("1W", 320f, 0.2f));
        this.bagList.Add(new Club("5I", 140f, 0.3f));
        this.bagList.Add(new Club("SW", 50f, 0.4f));
        this.bagList.Add(new Club("P", 30f, 0.125f));

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
