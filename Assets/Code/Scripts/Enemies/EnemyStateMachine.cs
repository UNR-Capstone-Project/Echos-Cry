using UnityEngine;

public class EnemyStateMachine 
{
    private EnemyState _currentState;
    private EnemyStateCache _stateCache;
    public EnemyStateMachine(EnemyState startState, EnemyStats enemyData)
    {
        _currentState = startState;
    }
    public void StateMachineStart()
    {
        _currentState.EnterState();
    }
    public void Update()
    {
        _currentState.UpdateState();
    }
    public void HandleSwitchState(EnemyState newState)
    {
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
}
