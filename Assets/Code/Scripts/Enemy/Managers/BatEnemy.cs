using UnityEngine;

public class BatEnemy : Enemy
{
    [Header("Config File")]
    [SerializeField] protected BatEnemyConfigFile _enemyConfigFile;

    public override void Init()
    {
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Spawn, 
            new BatSpawnState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Stagger,
            new BatStaggerState(_enemyConfigFile, this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Attack,
            new BatAttackState(_enemyConfigFile, this)
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
            new BatIdleState(_enemyConfigFile, this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Charge,
            new BatChargeState(_enemyConfigFile, this)
        );

        _stateMachine.Init(_stateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Spawn));
    }
}
