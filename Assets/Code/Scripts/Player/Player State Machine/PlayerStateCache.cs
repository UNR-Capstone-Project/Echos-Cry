using System.Collections.Generic;
using UnityEngine;

public class PlayerStateCache
{
    public PlayerStateCache(PlayerManager playerContext, PlayerStateMachine playerStateMachine)
    {
        _stateCache = new Dictionary<PlayerState, PlayerActionState>()
        {
            {PlayerState.IDLE, new PlayerIdleState(playerStateMachine, this) }
        };
    }

    public PlayerActionState RequestState(PlayerState state)
    {
        if(_stateCache.ContainsKey(state)) return _stateCache[state];
        else return null;
    }

    public enum PlayerState
    {
        NONE = 0,
        IDLE
    }

    private Dictionary<PlayerState, PlayerActionState> _stateCache;
}
