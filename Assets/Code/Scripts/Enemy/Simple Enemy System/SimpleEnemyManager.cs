//using UnityEngine;
//using UnityEngine.AI;
//using static SimpleEnemyStateCache;

////Main Handler script for enemy behavior and data.
////This script handles the instantiation of the State Machine and its logic

//[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(Animator))]
//[RequireComponent (typeof(Rigidbody))]
//[RequireComponent(typeof(BoxCollider))]
//[RequireComponent(typeof(EnemyBaseAttack))]
//[RequireComponent(typeof(EnemyDrops))]
//[RequireComponent(typeof(EnemyStats))]
//[RequireComponent(typeof(EnemySound))]
//[RequireComponent(typeof(EnemyAnimator))]
//[RequireComponent(typeof(EnemyCollisionHandler))]

//public class SimpleEnemyManager : MonoBehaviour
//{
//    private void SelectStartState()
//    {
//        switch (TypeOfEnemy)
//        {
//            case EnemyType.BAT:
//                //_enemyStateCache = new BatEnemyStateCache(this);
//                _enemyStateMachine.CurrentState  = _enemyStateCache.RequestState(EnemyStates.BAT_SPAWN);
//                break;
//            case EnemyType.RANGE:
//               // _enemyStateCache = new RangeEnemyStateCache(this);
//                _enemyStateMachine.CurrentState = _enemyStateCache.RequestState(EnemyStates.RANGE_SPAWN);
//                break;
//            case EnemyType.WALKER:
//                // _enemyStateCache = new RangeEnemyStateCache(this);
//                _enemyStateMachine.CurrentState = _enemyStateCache.RequestState(EnemyStates.WALKER_SPAWN);
//                break;
//            default:
//                break;
//        }
//    }
//    private void UpdateState02ms()
//    {
//        EnemyStateMachine.CurrentState.UpdateState02ms(this);
//    }
//    public void SwitchState(EnemyStates newState)
//    {
//        SimpleEnemyState state = _enemyStateCache.RequestState(newState);
//        if (state != null) _enemyStateMachine.HandleSwitchState(state, this);
//        else Debug.LogError("Invalid state request");
//    }

//    private void Awake()
//    {   
//        _enemyStateMachine = new();
//        if (_enemyStateCache == null) _enemyStateCache = new();

//        _enemyRigidbody  = GetComponent<Rigidbody>();
//        _enemyAnimator   = _enemySprite.GetComponent<Animator>();
//        _enemySound = GetComponent<EnemySound>();
//        _enemyNMA        = GetComponent<NavMeshAgent>();
//        _enemyStats      = GetComponent<EnemyStats>();
//        _enemyBaseAttack = GetComponent<EnemyBaseAttack>();
//    }
//    private void Start()
//    {
//        SelectStartState();
//        _enemyStateMachine.CurrentState.EnterState(this);

//        TickManager.OnTick02Event += UpdateState02ms;
//    }
//    private void OnDestroy()
//    {
//        TickManager.OnTick02Event -= UpdateState02ms;
//    }
//    private void Update()
//    {
//        _enemyStateMachine.Update(this);
//    }

//    public enum EnemyType
//    {
//        UNASSIGNED = 0, BAT, RANGE, WALKER
//    }

//    public EnemyType TypeOfEnemy = EnemyType.UNASSIGNED;
    
//    private static SimpleEnemyStateCache _enemyStateCache;
//    [SerializeField] private GameObject _enemySprite;
//    private Animator                _enemyAnimator;
//    private EnemySound              _enemySound;
//    private NavMeshAgent            _enemyNMA;
//    private EnemyStats              _enemyStats;
//    private SimpleEnemyStateMachine _enemyStateMachine;
//    private Rigidbody               _enemyRigidbody;
//    private EnemyBaseAttack         _enemyBaseAttack;

//    public static SimpleEnemyStateCache EnemyStateCache { get { return _enemyStateCache; } }

//    public Animator                EnemyAnimator     { get { return _enemyAnimator;     } }
//    public EnemySound              EnemySound        { get { return _enemySound; } }
//    public NavMeshAgent            EnemyNMA          { get { return _enemyNMA;          } }  
//    public EnemyStats              EnemyStats        { get { return _enemyStats;        } }
//    public SimpleEnemyStateMachine EnemyStateMachine { get { return _enemyStateMachine; } }
//    public Rigidbody               EnemyRigidbody    { get { return _enemyRigidbody;    } }
//    public EnemyBaseAttack         EnemyBaseAttack   { get { return _enemyBaseAttack;   } }
//}
