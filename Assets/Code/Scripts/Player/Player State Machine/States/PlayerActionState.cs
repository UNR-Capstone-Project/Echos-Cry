using System;
using UnityEngine;

public abstract class PlayerActionState : IState
{
    protected PlayerStateMachine _playerStateMachine;
    protected PlayerStateCache _playerStateCache;
    protected Player _playerContext;
    protected PlayerActionState(Player playerContext, PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache)
    {
        _playerContext = playerContext;
        _playerStateMachine = playerStateMachine;   
        _playerStateCache = playerStateCache;
        if (_playerStateMachine == null) Debug.LogError("Invalid value passed: " + GetType().ToString() + "::_playerContext");
        if (_playerStateMachine == null) Debug.LogError("Invalid value passed: " + GetType().ToString() + "::_playerStateMachine");
        if (_playerStateCache == null) Debug.LogError("Invalid value passed: " + GetType().ToString() + "::_playerStateCache");
    }

    public virtual void Update() { }
    public virtual void FixedUpdate(){ }
    public virtual void Enter() { }
    public virtual void Exit() { }
    public void CheckSwitch() 
    {
        if (IsDead()) _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Death));
        OnCheckSwitch();
    }
    protected virtual void OnCheckSwitch() { }

    private bool IsDead()
    {
        if (_playerContext.Stats.CurrentHealth <= 0) return true;
        else return false;
    }
}
