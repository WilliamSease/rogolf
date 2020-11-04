using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;

public class ShotCondition
{
    protected Func<int, int, MaterialType, Game, bool> condition;

    public ShotCondition() { }

    public ShotCondition(Func<int, int, MaterialType, Game, bool> condition)
    {
        this.condition = condition;
    }

    public bool Check(int par, int strokes, MaterialType terrain, Game game)
    {
        return condition(par, strokes, terrain, game);
    }
}
