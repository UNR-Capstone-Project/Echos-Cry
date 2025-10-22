using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyManager : MonoBehaviour
{
    private SimpleEnemyBehavior InitEnemyBehavior(EnemyType enemyType)
    {
        SimpleEnemyBehavior initBehavior = null;

        switch ((int)enemyType)
        {
            case (int)EnemyType.BASIC:
                initBehavior = new BasicEnemyBehavior(this, _enemyStateCache, _enemyStateMachine);
                break;
            default:
                break;
        }
        return initBehavior;
    }

    private void Awake()
    {
        EnemyAnimator = GetComponent<Animator>();
        EnemyNMAgent  = GetComponent<NavMeshAgent>();
        
        _enemyStateMachine = new SimpleEnemyStateMachine();
    }
    private void Start()
    {
        _enemyBehavior = InitEnemyBehavior(TypeOfEnemy);
        _enemyStateCache   = new SimpleEnemyStateCache(_enemyBehavior);

        _enemyStateMachine.CurrentState = _enemyStateCache.Spawn();
        _enemyStateMachine.CurrentState.EnterState();
    }
    private void Update()
    {
        _enemyStateMachine.Update();
    }

    public enum EnemyType
    {
        UNASSIGNED = 0, BASIC
    }
    private SimpleEnemyBehavior     _enemyBehavior;
    private SimpleEnemyStateMachine _enemyStateMachine;
    private SimpleEnemyStateCache   _enemyStateCache;

    public EnemyType TypeOfEnemy = EnemyType.UNASSIGNED;

    public Animator     EnemyAnimator;
    public NavMeshAgent EnemyNMAgent;
}
