using System.Collections.Generic;
using UnityEngine;

public class PlayerStateCache
{
    private Dictionary<PlayerStateMachine.PlayerState, PlayerAbstractState> _stateCache;
    public PlayerStateCache(PlayerManager playerContext)
    {
        _stateCache = new Dictionary<PlayerStateMachine.PlayerState, PlayerAbstractState>()
        {
            {PlayerStateMachine.PlayerState.NONE, null }
        };
    }
    public PlayerAbstractState RequestState(PlayerStateMachine.PlayerState state)
    {
        if(_stateCache.ContainsKey(state)) return _stateCache[state];
        else return null;
    }
}
