
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] protected AttackStrategy[] _attackStrategies;
    [SerializeField] protected TargetStrategy[]   _targetStrategy;
    [SerializeField] protected MovementStrategy[] _movementStrategy;
    [SerializeField] protected ItemDropStrategy _drops;

    public EnemyStats Stats { get => _stats; }
    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }
    public Rigidbody Rigidbody { get => _rigidbody; }
    public Animator Animator { get => _animator; }

    public EnemyStateCache StateCache { get => _stateCache; }
    public EnemyStateMachine StateMachine { get => _stateMachine; }
    public AttackStrategy[] AttackStrategies { get => _attackStrategies; }
    public TargetStrategy[] TargetStrategy { get => _targetStrategy; }
    public MovementStrategy[] MovementStrategy { get => _movementStrategy; }
    public ItemDropStrategy DropsStrategy { get => _drops; }

    public abstract void Init();

    protected virtual void Awake()
    {   
        _stateMachine = new(this);
        _stateCache = new();
    }
    protected virtual void Start()
    {
        TickManager.OnTick02Event += _stateMachine.TickState;
    }
    protected virtual void OnDestroy()
    {
        TickManager.OnTick02Event -= _stateMachine.TickState;
    }

    protected virtual void Update()
    {
        _stateMachine.Update();
    }
}
