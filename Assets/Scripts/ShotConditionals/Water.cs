using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : ShotConditional
{
    public Water()
    {
        conditional = (p, s, t, g) => 
        {
            HoleData hole = g.GetHoleBag().GetCurrentHoleData();

            if (!hole.GetWater() && t == MaterialType.WATER)
            {
                hole.SetWater();
                return new Tuple<string, int>("Water", -5);
            }
            else
            {
                return null;
            }
        };
    }
}
