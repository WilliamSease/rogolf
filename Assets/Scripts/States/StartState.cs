using UnityEngine;

public class StartState : State
{
    public StartState(Game game) : base(game) { }

    public override void Tick()
    {
        // TODO : Initialize important game stuff here
    }

    public override void OnKeyReturn()
    {
        UnityEngine.Debug.Log("Going to IdleState!");
        game.SetState(new IdleState(game));
    }
}
