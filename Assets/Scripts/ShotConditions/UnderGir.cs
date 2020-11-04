using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;

public class UnderGir : ShotCondition
{
    public UnderGir()
    {
        condition = (p, s, t, g) => s <= p - 3 && t == MaterialType.GREEN; 
    }
}
