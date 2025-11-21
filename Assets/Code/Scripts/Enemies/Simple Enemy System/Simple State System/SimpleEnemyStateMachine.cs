//DO NOT ADJUST UNLESS STRICTLY NECESSARY
public class SimpleEnemyStateMachine 
{
    public SimpleEnemyState CurrentState { get; set; }
    private SimpleEnemyManager _enemyContext;

    public SimpleEnemyStateMachine(SimpleEnemyManager enemyContext)
    {
        _enemyContext = enemyContext; 
    }
    public void HandleSwitchState(SimpleEnemyState newState)
    {
        CurrentState.ExitState(_enemyContext);
        CurrentState = newState;
        CurrentState.EnterState(_enemyContext);
    }
    public void Update()
    {
        CurrentState.CheckSwitchState(_enemyContext);
        CurrentState.UpdateState(_enemyContext);
    }
}
