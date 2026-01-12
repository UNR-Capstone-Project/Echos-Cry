using System;
using UnityEngine;

public abstract class PlayerActionState : IState
{
    protected PlayerStateMachine _playerStateMachine;
    protected PlayerStateCache _playerStateCache;
    protected PlayerActionState(PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache)
    {
        _playerStateMachine = playerStateMachine;   
        _playerStateCache = playerStateCache;
    }

    public virtual void UpdateState() { }
    public virtual void FixedUpdateState(){ }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void CheckSwitchState() { }

    protected void RequestSwitchState(PlayerStateCache.PlayerState newState) 
    {
        _playerStateMachine.SwitchState(_playerStateCache.RequestState(newState));
    }

}
