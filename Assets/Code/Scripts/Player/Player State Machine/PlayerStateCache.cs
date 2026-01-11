using System.Collections.Generic;
using UnityEngine;

public class PlayerStateCache
{
    public PlayerStateCache(PlayerManager playerContext, PlayerStateMachine playerStateMachine)
    {
        _stateCache = new Dictionary<PlayerState, PlayerActionState>()
        {
            {PlayerState.Idle, new PlayerIdleState(playerStateMachine, this) }
        };
    }

    public PlayerActionState RequestState(PlayerState state)
    {
        if(_stateCache.ContainsKey(state)) return _stateCache[state];
        else return null;
    }

    public enum PlayerState
    {
        None = 0,
        Idle, Move, Attack, Dash
    }

    private Dictionary<PlayerState, PlayerActionState> _stateCache;
}
