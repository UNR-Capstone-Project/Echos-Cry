using UnityEngine;
using UnityEngine.AI;
using static SimpleEnemyStateCache;

//Main Handler script for enemy behavior and data.
//This script handles the instantiation of the State Machine and its logic

public class SimpleEnemyManager : MonoBehaviour
{
    public void SelectStartState()
    {
        switch (TypeOfEnemy)
        {
            case EnemyType.BAT:
                _enemyStateCache = new BatEnemyStateCache(this);
                _enemyStateMachine.CurrentState  = _enemyStateCache.RequestState(States.BAT_SPAWN);
                break;
            case EnemyType.RANGE:
                _enemyStateCache = new RangeEnemyStateCache(this);
                _enemyStateMachine.CurrentState = _enemyStateCache.RequestState(States.RANGE_SPAWN);
                break;
            default:
                break;
        }
    }
    public void SwitchState(States newState)
    {
        SimpleEnemyState state = _enemyStateCache.RequestState(newState);
        if (state != null) _enemyStateMachine.HandleSwitchState(state);
        else Debug.LogError("Invalid state request");
    }

    private void Awake()
    {   
        _enemyStateMachine = new();

        _enemyRigidbody  = GetComponent<Rigidbody>();
        _enemyAnimator   = GetComponent<Animator>();
        _enemyNMA        = GetComponent<NavMeshAgent>();
        _enemyStats      = GetComponent<EnemyStats>();
        _enemyBaseAttack = GetComponent<EnemyBaseAttack>();
    }
    private void Start()
    {
        SelectStartState();
        _enemyStateMachine.CurrentState.EnterState();
    }
    private void Update()
    {
        _enemyStateMachine.Update();
    }

    public enum EnemyType
    {
        UNASSIGNED = 0, BAT, RANGE
    }

    public EnemyType TypeOfEnemy = EnemyType.UNASSIGNED;
    
    private SimpleEnemyStateCache   _enemyStateCache;
    private Animator                _enemyAnimator;
    private NavMeshAgent            _enemyNMA;
    private EnemyStats              _enemyStats;
    private SimpleEnemyStateMachine _enemyStateMachine;
    private Rigidbody               _enemyRigidbody;
    private EnemyBaseAttack         _enemyBaseAttack;

    public SimpleEnemyStateCache   EnemyStateCache   { get { return _enemyStateCache;   } }
    public Animator                EnemyAnimator     { get { return _enemyAnimator;     } }
    public NavMeshAgent            EnemyNMA          { get { return _enemyNMA;          } }  
    public EnemyStats              EnemyStats        { get { return _enemyStats;        } }
    public SimpleEnemyStateMachine EnemyStateMachine { get { return _enemyStateMachine; } }
    public Rigidbody               EnemyRigidbody    { get { return _enemyRigidbody;    } }
    public EnemyBaseAttack         EnemyBaseAttack   { get { return _enemyBaseAttack;   } }
}
