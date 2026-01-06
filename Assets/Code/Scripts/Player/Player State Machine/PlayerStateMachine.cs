using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerStateMachine(PlayerManager playerContext)
    {
        _stateCache = new PlayerStateCache(playerContext);
    }

    public void SwitchStates(PlayerState newState)
    {
        _currentState.ExitState();
        _currentState = _stateCache.RequestState(newState);
        _currentState?.EnterState();
    }
    public void Update()
    {
        _currentState?.UpdateState();
    }

    public enum PlayerState
    {
        NONE = 0,

    }
    private PlayerAbstractState _currentState;
    private PlayerStateCache    _stateCache;
}
