using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Enemy/Cache Strategy/Range")]
public class RangeCacheStrategy : EnemyCacheStrategy
{
    [SerializeField] private RangeData _data;

    public override void Execute(EnemyStateCache stateCache, Enemy enemyContext)
    {
        stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeSpawn,
            new RangeSpawnState(enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeStagger,
            new RangeStaggerState(_data, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeAttack,
            new RangeAttackState(_data, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeRoam,
            new RangeRoamState(_data, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeDeath,
            new RangeDeathState(enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeIdle,
            new RangeIdleState(_data, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.RangeCharge,
            new RangeChargeAttackState(_data, enemyContext)
        );
        stateCache.StartState = stateCache.RequestState(EnemyStateCache.EnemyStates.RangeSpawn);
    }
}
