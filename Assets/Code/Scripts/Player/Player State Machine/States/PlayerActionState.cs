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
        if (_playerStateMachine == null) Debug.LogError("Invalid value passed: " + GetType().ToString() + "::_playerStateMachine");
        if (_playerStateCache == null) Debug.LogError("Invalid value passed: " + GetType().ToString() + "::_playerStateCache");
    }

    public virtual void UpdateState() { }
    public virtual void FixedUpdateState(){ }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void CheckSwitchState() { }

    protected void RequestSwitchState(PlayerStateCache.PlayerState newState) 
    {
        if (_playerStateCache == null || _playerStateMachine == null) return;
        _playerStateMachine.SwitchState(_playerStateCache.RequestState(newState));
    }

}
