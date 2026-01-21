using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected EnemyStateCache _stateCache;
    protected EnemyStateMachine _stateMachine;

    [Header("Enemy-Related Components")]
    [SerializeField] protected EnemyStats _stats;
    [SerializeField] protected NavMeshAgent _navMeshAgent;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Animator _animator;

    [Header("Strategies")]
    [SerializeField] protected AttackStrategy[] _attackStrats;
    [SerializeField] protected TargetStrategy[]   _targetStrats;
    [SerializeField] protected MovementStrategy[] _movementStrats;
    [SerializeField] protected ItemDropStrategy _drops;

    public EnemyStats Stats { get => _stats; }
    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }
    public Rigidbody Rigidbody { get => _rigidbody; }
    public Animator Animator { get => _animator; }

    public EnemyStateCache StateCache { get => _stateCache; }
    public EnemyStateMachine StateMachine { get => _stateMachine; }
    public AttackStrategy[] AttackStrategies { get => _attackStrats; }
    public TargetStrategy[] TargetStrategy { get => _targetStrats; }
    public MovementStrategy[] MovementStrategy { get => _movementStrats; }
    public ItemDropStrategy DropsStrategy { get => _drops; }

    public abstract void Init();

    protected virtual void Awake()
    {   
        _stateMachine = new();
        _stateCache = new();
    }
    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        _stateMachine.UpdateState();
    }
}
