using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : ShotConditional
{
    public Bunker()
    {
        conditional = (p, s, t, g) => 
        {
            HoleData hole = g.GetHoleBag().GetCurrentHoleData();

            if (!hole.GetBunker() && t == MaterialType.BUNKER)
            {
                hole.SetBunker();
                return new Tuple<string, int>("Bunker", -2);
            }
            else
            {
                return null;
            }
        };
    }
}
