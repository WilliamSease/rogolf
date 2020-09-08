using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameState : State
{
    public StartGameState(Game game) : base(game) { }

    public override void Tick()
    {
        // Initialize game data
        game.holeBag = new HoleBag();
        game.itemBag = new ItemBag();

        // Advance state
        game.SetState(new StartHoleState(game));
    }
}
