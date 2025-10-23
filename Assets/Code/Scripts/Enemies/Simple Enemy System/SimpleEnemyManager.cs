using UnityEngine;
using UnityEngine.AI;

//Main Handler script for enemy behavior and data.
//This script handles the instantiation of the State Machine and its logic


public class SimpleEnemyManager : MonoBehaviour
{
    private void Awake()
    {   
        _enemyBehavior = GetComponent<SimpleEnemyBehavior>();
        if (_enemyBehavior == null)
        {
            Debug.LogError("Enemy does not have behavior component! Disabling GameObject: " + gameObject.name);
            gameObject.SetActive(false);
        }

        _enemyStateMachine = new SimpleEnemyStateMachine();
        _enemyStateCache   = new SimpleEnemyStateCache(_enemyBehavior);
    }
    private void Start()
    {
        _enemyStateMachine.CurrentState = _enemyStateCache.Spawn();
        _enemyStateMachine.CurrentState.EnterState();
    }
    private void Update()
    {
        _enemyStateMachine.Update();
    }

    private SimpleEnemyBehavior     _enemyBehavior;
    private SimpleEnemyStateMachine _enemyStateMachine;
    private SimpleEnemyStateCache   _enemyStateCache;
    public SimpleEnemyStateCache   EnemyStateCache { get { return _enemyStateCache; } }
    public SimpleEnemyStateMachine EnemyStateMachine { get { return _enemyStateMachine; } }
}
