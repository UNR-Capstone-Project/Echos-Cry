using System.Collections.Generic;
using UnityEngine;

public class PlayerStateCache
{
    public enum PlayerState
    {
        None = 0,
        Idle, Move, Attack, Dash
    }

    private Dictionary<PlayerState, PlayerActionState> _stateCache;
    
    public PlayerStateCache()
    {
        _stateCache = new Dictionary<PlayerState, PlayerActionState>();
    }

    public void Init(PlayerManager playerContext, PlayerStateMachine playerStateMachine)
    {
        _stateCache.Add(
            PlayerState.Idle,
            new PlayerIdleState(
                playerStateMachine, 
                this)
        );
        _stateCache.Add(
            PlayerState.Move,
            new PlayerMoveState(
                playerContext.PlayerMovement,
                playerContext.PlayerInputHandler,
                playerContext.PlayerAnimator,
                playerStateMachine,
                this)
        );
        _stateCache.Add(
            PlayerState.Attack,
            new PlayerAttackState(
                playerStateMachine, 
                this)
        );
        _stateCache.Add(
            PlayerState.Dash,
            new PlayerDashState(
                playerStateMachine,
                this)
        );
    }

    public PlayerActionState RequestState(PlayerState state)
    {
        if(_stateCache.ContainsKey(state)) return _stateCache[state];
        else return null;
    }

}
