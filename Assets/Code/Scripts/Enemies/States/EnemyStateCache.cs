using UnityEngine;

public class EnemyStateCache 
{
    enum States
    {
        
    }
    private const int ARRAY_SIZE = 5;
    private EnemyState[] _enemyStates;
    public EnemyStateCache(EnemyStateMachine stateMachineContext)
    {
        _enemyStates = new EnemyState[ARRAY_SIZE];
    }
}
