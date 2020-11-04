using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDrive : ShotConditional
{
    public LongDrive()
    {
        conditional = (p, s, t, g) => 
        {
            HoleData hole = g.GetHoleBag().GetCurrentHoleData();

            bool longDrive = s == 1 && g.GetBall().DistanceFromTee() > 320f;
            if (hole.GetFir() && longDrive)
            {
                return new Tuple<string, int>("Long Drive", 2);
            }
            else
            {
                return null;
            }
        };
    }
}
