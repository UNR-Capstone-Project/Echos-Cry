using UnityEngine;

public class WalkerEnemy : Enemy
{
    [SerializeField] private WalkerEnemyConfigFile _config;
    public override void Init()
    {
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerSpawn,
            new WalkerSpawnState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerStagger,
            new WalkerStaggerState(_config, this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerAttack,
            new WalkerAttackState(_config, this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerChase,
            new WalkerChaseState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerDeath,
            new WalkerDeathState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerIdle,
            new WalkerIdleState(_config, this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerCharge,
            new WalkerChargeAttackState(_config, this)
        );

        _stateMachine.Init(_stateCache.RequestState(EnemyStateCache.EnemyStates.WalkerSpawn));

        _navMeshAgent.stoppingDistance = _config.StoppingDistance;
    }
}
