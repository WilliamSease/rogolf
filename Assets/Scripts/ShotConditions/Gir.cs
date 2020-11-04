using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;

public class Gir : ShotCondition
{
    public Gir()
    {
        condition = (p, s, t, g) => s <= p - 2 && t == MaterialType.GREEN; 
    }
}
