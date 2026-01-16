using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected EnemyStateCache   _enemyStateCache;
    protected EnemyStateMachine _enemyStateMachine;

    protected EnemyStats _enemyStats;

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
            new BatSpawnState(_enemyStateMachine, _enemyStateCache));
    }
}
