using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Enemy/Cache Strategy/Bat")]
public class BatCacheStrategy : EnemyCacheStrategy
{
    [SerializeField] private BatData _data;

    public override void Execute(EnemyStateCache stateCache, Enemy enemyContext)
    {
        stateCache.AddState(
            EnemyStateCache.EnemyStates.BatSpawn,
            new BatSpawnState(enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.BatStagger,
            new BatStaggerState(_data, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.BatAttack,
            new BatAttackState(_data, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.BatChase,
            new BatChaseState(enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.BatDeath,
            new BatDeathState(enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.BatIdle,
            new BatIdleState(_data, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.BatCharge,
            new BatChargeState(_data, enemyContext)
        );
        stateCache.StartState = stateCache.RequestState(EnemyStateCache.EnemyStates.BatSpawn);
    }
}
