using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState : State
{
    private Item item;

    public ItemState(Game game) : base(game) {
        item = null;
    }

    public override void OnStateEnter()
    {
        // TODO - show items and stuff
    }

    public override void OnStateExit()
    {
        // TODO - hide item sceen and stuff
    }

    public override void Tick() { }

    /// <summary>
    /// Selects the right item.
    /// </summary>
    public override void OnKeyRightArrow()
    {
        // item = rightItem
        // update some visuals
        item = new PowerUp();
    }

    /// <summary>
    /// Selects the left item.
    /// </summary>
    public override void OnKeyLeftArrow()
    {
        // item = leftItem;
        // update some visuals
        item = new PowerUp();
    }

    /// <summary>
    /// Confirms player's selected item.
    /// Player must have item selected to confirm.
    /// </summary>
    public override void OnKeyReturn()
    {
        // Player must select item before confirming
        if (item != null)
        {
            // TODO - this is under construction
            // I'm not sure if ItemState is even going to be a state anymore
            game.SetState(new NoState(game));
        }
    }
}
