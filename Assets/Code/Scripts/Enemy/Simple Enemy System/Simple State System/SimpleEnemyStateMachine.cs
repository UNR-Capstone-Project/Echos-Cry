//DO NOT ADJUST UNLESS STRICTLY NECESSARY
public class SimpleEnemyStateMachine 
{
    public SimpleEnemyState CurrentState { get; set; }

    public SimpleEnemyStateMachine() { }

    public void HandleSwitchState(SimpleEnemyState newState, SimpleEnemyManager enemyContext)
    {
        CurrentState.ExitState(enemyContext);
        CurrentState = newState;
        CurrentState.EnterState(enemyContext);
    }
    public void Update(SimpleEnemyManager enemyContext)
    {
        CurrentState.CheckSwitchState(enemyContext);
        CurrentState.UpdateState(enemyContext);
    }
}
