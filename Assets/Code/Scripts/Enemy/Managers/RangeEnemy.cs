using UnityEngine;

public class RangeEnemy : Enemy
{
    [Header("Config File")]
    [SerializeField] protected RangeEnemyConfigFile _enemyConfigFile;

    public override void Init()
    {
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeSpawn,
            new RangeSpawnState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeStagger,
            new RangeStaggerState(_enemyConfigFile, this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeAttack,
            new RangeAttackState(_enemyConfigFile, this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeRoam,
            new RangeRoamState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeDeath,
            new RangeDeathState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeIdle,
            new RangeIdleState(_enemyConfigFile, this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeCharge,
            new RangeChargeAttackState(_enemyConfigFile, this)
        );

        _stateMachine.Init(_stateCache.RequestState(EnemyStateCache.EnemyStates.RangeSpawn));
    }
}