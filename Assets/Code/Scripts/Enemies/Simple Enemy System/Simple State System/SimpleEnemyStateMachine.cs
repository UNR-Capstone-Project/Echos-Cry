//DO NOT ADJUST UNLESS STRICTLY NECESSARY
public class SimpleEnemyStateMachine 
{
    public SimpleEnemyState CurrentState { get; set; }

    public SimpleEnemyStateMachine() { }

    public void HandleSwitchState(SimpleEnemyState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
    public void Update()
    {
        CurrentState.CheckSwitchState();
        CurrentState.UpdateState();
    }
}
