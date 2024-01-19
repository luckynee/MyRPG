
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerBaseState
{
    protected PlayerStateMachine _ctx;
    protected PlayerStateFactory _factory;
    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchState();

    public abstract void InitializeSubState();

    void UpdateStates() { }

    protected void SwitchState(PlayerBaseState newState) 
    {
        //current state exits state
        ExitState();

        //new state enter state
        newState.EnterState();

        //siwtch current state of context
        _ctx.CurrentState = newState;
    }

    protected void SetSuperState() { }

    protected void SetSubState() { }
   
}
