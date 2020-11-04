using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;

public class Fir : ShotCondition
{
    public Fir()
    {
        condition = (p, s, t, g) => s == 1 && t == MaterialType.FAIRWAY; 
    }
}
