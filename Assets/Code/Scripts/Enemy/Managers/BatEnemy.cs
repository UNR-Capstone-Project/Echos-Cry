using UnityEngine;

public class BatEnemy : Enemy
{
    [Header("Config File")]
    [SerializeField] protected BatEnemyConfigFile _enemyConfigFile;

    public override void Init()
    {
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.BatSpawn, 
            new BatSpawnState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.BatStagger,
            new BatStaggerState(_enemyConfigFile, this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.BatAttack,
            new BatAttackState(_enemyConfigFile, this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.BatChase,
            new BatChaseState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.BatDeath,
            new BatDeathState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.BatIdle,
            new BatIdleState(_enemyConfigFile, this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.BatCharge,
            new BatChargeState(_enemyConfigFile, this)
        );

        _stateMachine.Init(_stateCache.RequestState(EnemyStateCache.EnemyStates.BatSpawn));
    }
}
