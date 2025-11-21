using UnityEngine;
using UnityEngine.AI;
using static SimpleEnemyStateCache;

//Main Handler script for enemy behavior and data.
//This script handles the instantiation of the State Machine and its logic

public class SimpleEnemyManager : MonoBehaviour
{
    private void Awake()
    {   
        _enemyStateMachine = new(this);
        EnemyStateCache ??= new();

        _enemyRigidbody = GetComponent<Rigidbody>();
        _enemyAnimator  = GetComponent<Animator>();
        _enemyNMA       = GetComponent<NavMeshAgent>();
        _enemyStats     = GetComponent<EnemyStats>();
    }
    private void Start()
    {
        _enemyStateMachine.CurrentState = RequestState(EnemyStartState);
        _enemyStateMachine.CurrentState.EnterState(this);
    }
    private void Update()
    {
        _enemyStateMachine.Update();
    }

    public States EnemyStartState = States.UNASSIGNED;
    
    public static SimpleEnemyStateCache   EnemyStateCache { get; private set; }

    private Animator                _enemyAnimator;
    private NavMeshAgent            _enemyNMA;
    private EnemyStats              _enemyStats;
    private SimpleEnemyStateMachine _enemyStateMachine;
    private Rigidbody               _enemyRigidbody;

    public Animator                EnemyAnimator     { get { return _enemyAnimator;     } }
    public NavMeshAgent            EnemyNMA          { get { return _enemyNMA;          } }  
    public EnemyStats              EnemyStats        { get { return _enemyStats;        } }
    public SimpleEnemyStateMachine EnemyStateMachine { get { return _enemyStateMachine; } }
    public Rigidbody               EnemyRigidbody    { get { return _enemyRigidbody;    } }
}
