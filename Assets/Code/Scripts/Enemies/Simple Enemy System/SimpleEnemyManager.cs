using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyManager : MonoBehaviour
{
    private void Awake()
    {
        EnemyAnimator      = GetComponent<Animator>();
        EnemyNMAgent       = GetComponent<NavMeshAgent>();
        
        _enemyBehavior     = GetComponent<SimpleEnemyBehavior>();
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

    public Animator     EnemyAnimator;
    public NavMeshAgent EnemyNMAgent;
}
