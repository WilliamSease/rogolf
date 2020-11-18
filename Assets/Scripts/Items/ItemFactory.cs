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
            case "TurfRoller": return new TurfRoller();
            case "Links": return new Links();
            case "Rake": return new Rake();
            case "SandSaver": return new SandSaver();
            case "PristineFairways": return new PristineFairways();
            case "SharpBlades": return new SharpBlades();
            case "PerformanceTees": return new PerformanceTees();
            case "RedDice": return new RedDice();
            case "BlueDice": return new BlueDice();
            case "GreenDice": return new GreenDice();
            case "PowerDown": return new PowerDown();
            case "ControlDown": return new ControlDown();
            case "ImpactDown": return new ImpactDown();
            case "SpinDown": return new SpinDown();
            case "HundredYearFlood": return new HundredYearFlood();
            default: throw new Exception(String.Format("Item {0} does not exist in the lookup", name)); 
        }
    }
}
