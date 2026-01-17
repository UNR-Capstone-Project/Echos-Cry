using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected EnemyStateCache   _enemyStateCache;
    protected EnemyStateMachine _enemyStateMachine;

    [SerializeField] protected EnemyStats _stats;
    [SerializeField] protected NavMeshAgent _navMeshAgent;
    public EnemyStats Stats { get => _stats; }
    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }

    protected AttackStrategy   _attackStrategy;
    protected TargetStrategy   _targetStrategy;
    protected MovementStrategy _movementStrategy;


    public virtual void Init(
        AttackStrategy atckStrat, 
        TargetStrategy trgtStrat, 
        MovementStrategy moveStrat)
    {
        _attackStrategy = atckStrat;
        _targetStrategy = trgtStrat;
        _movementStrategy = moveStrat;
    }

    protected virtual void TickState() { }

    protected virtual void Awake()
    {   
        _enemyStateMachine = new();
        _enemyStateCache = new();
    }
    protected virtual void Start()
    {
        TickManager.OnTick02Event += TickState;
    }
    protected virtual void OnDestroy()
    {
        TickManager.OnTick02Event -= TickState;
    }

    protected virtual void Update()
    {
        _enemyStateMachine.Update();
    }
}
public class BatEnemy : Enemy
{
    public override void Init(AttackStrategy atckStrat, TargetStrategy trgtStrat, MovementStrategy moveStrat)
    {
        base.Init(atckStrat, trgtStrat, moveStrat);

        _enemyStateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Spawn, 
            new BatSpawnState(_enemyStateMachine, _enemyStateCache)
        );
        _enemyStateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Stagger,
            new BatStaggerState(_enemyStateMachine, _enemyStateCache)
        );
        _enemyStateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Attack,
            new BatAttackState(_enemyStateMachine, _enemyStateCache)
        );
        _enemyStateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Chase,
            new BatChaseState(_enemyStateMachine, _enemyStateCache)
        );
        _enemyStateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Death,
            new BatDeathState(_enemyStateMachine, _enemyStateCache)
        );
        _enemyStateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Idle,
            new BatIdleState(_enemyStateMachine, _enemyStateCache)
        );
        _enemyStateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Charge,
            new BatChargeState(_enemyStateMachine, _enemyStateCache)
        );

        _enemyStateMachine.Init(_enemyStateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Spawn));
    }
}
