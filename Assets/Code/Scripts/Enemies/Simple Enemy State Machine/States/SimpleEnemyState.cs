using System;

public abstract class SimpleEnemyState
{
    protected SimpleEnemyStateMachine _stateMachineContext;
    public SimpleEnemyState(SimpleEnemyStateMachine stateMachineContext)
    {
        _stateMachineContext = stateMachineContext;
        SwitchStateEvent += _stateMachineContext.HandleSwitchState;
    }
    ~SimpleEnemyState()
    {
        SwitchStateEvent -= _stateMachineContext.HandleSwitchState;
    }
    public event Action<SimpleEnemyState> SwitchStateEvent;
    public abstract void UpdateState();
    public abstract void EnterState();
    public abstract void ExitState();
    protected abstract void CheckSwitchState();
}
