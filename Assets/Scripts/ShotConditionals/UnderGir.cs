using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderGir : ShotConditional
{
    public UnderGir()
    {
        conditional = (p, s, t, g) => 
        {
            HoleData hole = g.GetHoleBag().GetCurrentHoleData();

            bool gir = s <= p - 3 && t == MaterialType.GREEN;
            if (!hole.GetGir() && gir)
            {
                hole.SetGir();
                return new Tuple<string, int>("Under GIR", 20);
            }
            else
            {
                return null;
            }
        };
    }
}
