using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotConditional
{
    protected Func<int, int, MaterialType, Game, Tuple<string, int>> conditional;

    public ShotConditional() { }

    public ShotConditional(Func<int, int, MaterialType, Game, Tuple<string, int>> conditional)
    {
        this.conditional = conditional;
    }

    public Tuple<string, int> Execute(int par, int strokes, MaterialType terrain, Game game)
    {
        return conditional(par, strokes, terrain, game);
    }
}
