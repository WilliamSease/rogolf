using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemFactory
{
    public static Item Create(string name)
    {
        switch (name)
        {
            case "PowerUp": return new PowerUp();
            case "ControlUp": return new ControlUp();
            case "ImpactUp": return new ImpactUp();
            case "SpinUp": return new SpinUp();
            case "FlashFlood": return new FlashFlood();
            case "Drought": return new Drought();
            default: throw new Exception(String.Format("Item {0} does not exist in the lookup", name)); 
        }
    }
}
