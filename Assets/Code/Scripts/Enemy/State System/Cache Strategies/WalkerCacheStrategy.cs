using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Enemy/Cache Strategy/Walker")]
public class WalkerCacheStrategy : EnemyCacheStrategy
{
    [SerializeField] private WalkerData _data;

    public override void Execute(EnemyStateCache stateCache, Enemy enemyContext)
    {
        stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerSpawn,
            new WalkerSpawnState(enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerStagger,
            new WalkerStaggerState(_data, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerAttack,
            new WalkerAttackState(_data, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerChase,
            new WalkerChaseState(enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerDeath,
            new WalkerDeathState(enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerIdle,
            new WalkerIdleState(_data, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.WalkerCharge,
            new WalkerChargeAttackState(_data, enemyContext)
        );
        stateCache.StartState = stateCache.RequestState(EnemyStateCache.EnemyStates.WalkerSpawn);
    }
}