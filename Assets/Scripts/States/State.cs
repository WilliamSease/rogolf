using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Game game;

    public State(Game game)
    {
        this.game = game;
    }

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public abstract void Tick();

    // Key Press Methods
    public virtual void OnKeySpace() { }
    public virtual void OnKeyUpArrow() { }
    public virtual void OnKeyDownArrow() { }
    public virtual void OnKeyRightArrow() { }
    public virtual void OnKeyLeftArrow() { }
    public virtual void OnKeyReturn() { }
}
