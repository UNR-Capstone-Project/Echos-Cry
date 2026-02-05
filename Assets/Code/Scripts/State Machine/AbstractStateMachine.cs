using UnityEngine;

public abstract class AbstractStateMachine<T> where T : IState
{
    protected T _currentState;

    public void Init(T initState)
    {
        _currentState = initState;
        _currentState?.Enter();
        OnInit();
    }
    protected virtual void OnInit() { }
    public virtual void UpdateState()
    {
        if (_currentState == null) return;
        _currentState.CheckSwitch();
        _currentState.Update();
    }
    public virtual void FixedUpdateState()
    {
        _currentState?.FixedUpdate();
    }
    public virtual void SwitchState(T newState)
    {
        if(_currentState == null) return;   
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}