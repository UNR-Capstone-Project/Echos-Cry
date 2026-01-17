
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected EnemyStateCache _stateCache;
    protected EnemyStateMachine _stateMachine;

    [SerializeField] protected EnemyStats _stats;
    [SerializeField] protected NavMeshAgent _navMeshAgent;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Animator _animator;

    protected List<IAttackStrategy> _attackStrategies;
    protected TargetStrategy   _targetStrategy;
    protected MovementStrategy _movementStrategy;

    public EnemyStats Stats { get => _stats; }
    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }
    public Rigidbody Rigidbody { get => _rigidbody; }
    public Animator Animator { get => _animator; }

    public EnemyStateCache StateCache { get => _stateCache; }
    public EnemyStateMachine StateMachine { get => _stateMachine; }
    public List<IAttackStrategy> AttackStrategies { get => _attackStrategies; }
    public TargetStrategy TargetStrategy { get => _targetStrategy; }
    public MovementStrategy MovementStrategy { get => _movementStrategy; }

    public virtual void Init(
        IAttackStrategy[] atckStrats, 
        TargetStrategy trgtStrat, 
        MovementStrategy moveStrat)
    {
        _attackStrategies = atckStrats.ToList();
        _targetStrategy = trgtStrat;
        _movementStrategy = moveStrat;
    }

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
public class BatEnemy : Enemy
{
    public override void Init(IAttackStrategy[] atckStrats, TargetStrategy trgtStrat, MovementStrategy moveStrat)
    {
        base.Init(atckStrats, trgtStrat, moveStrat);

        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Spawn, 
            new BatSpawnState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Stagger,
            new BatStaggerState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Attack,
            new BatAttackState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Chase,
            new BatChaseState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Death,
            new BatDeathState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Idle,
            new BatIdleState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Charge,
            new BatChargeState(this)
        );

        _stateMachine.Init(_stateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Spawn));
    }
}
