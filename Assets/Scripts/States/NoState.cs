using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoState : State
{
    public NoState(Game game) : base(game) { }

    public override void Tick() { }
}
