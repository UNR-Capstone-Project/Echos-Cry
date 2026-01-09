using UnityEngine;

public abstract class AbstractStateMachine<T> where T : IState
{
    protected T _currentState;

    public virtual void Init(T initState)
    {
        _currentState = initState;
        _currentState?.EnterState();
    }
    public virtual void Update()
    {
        if (_currentState == null) return;
        _currentState.CheckSwitchState();
        _currentState.UpdateState();
    }
    public void SwitchState(T newState)
    {
        if(_currentState == null) return;   
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
}