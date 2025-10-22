public class SimpleEnemyStateMachine 
{
    private SimpleEnemyState _currentState;

    public SimpleEnemyState CurrentState { get { return _currentState; } set { _currentState = value; } }

    public void HandleSwitchState(SimpleEnemyState newState)
    {
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
    public void Update()
    {
        _currentState.CheckSwitchState();
        _currentState.UpdateState();
    }
}
