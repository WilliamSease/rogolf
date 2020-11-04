using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gir : ShotConditional
{
    public Gir()
    {
        conditional = (p, s, t, g) => 
        {
            HoleData hole = g.GetHoleBag().GetCurrentHoleData();

            bool gir = s <= p - 2 && t == MaterialType.GREEN;
            if (!hole.GetGir() && gir)
            {
                hole.SetGir();
                return new Tuple<string, int>("GIR", 10);
            }
            else
            {
                return null;
            }
        };
    }
}
