using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fir : ShotConditional
{
    public Fir()
    {
        conditional = (p, s, t, g) => 
        {
            HoleData hole = g.GetHoleBag().GetCurrentHoleData();

            bool fir = s == 1 && t == MaterialType.FAIRWAY;
            if (!hole.GetFir() && fir)
            {
                hole.SetFir();
                return new Tuple<string, int>("FIR", 5);
            }
            else
            {
                return null;
            }
        };
    }
}
