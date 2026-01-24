
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/State Machine/Enemy Machine")]
public class EnemyStateMachine : AbstractStateMachine<EnemyState>
{

}

public interface IEnemyCacheStrategy
{
    public void Execute(EnemyStateCache stateCache, Enemy enemyContext);
}

public class BatCacheStrategy : IEnemyCacheStrategy
{
    private readonly BatConfig _config;
    public BatCacheStrategy(BatConfig config)
    {
        _config = config;
    }

    public void Execute(EnemyStateCache stateCache, Enemy enemyContext)
    {
        stateCache.AddState(
            EnemyStateCache.EnemyStates.BatSpawn,
            new BatSpawnState(enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.BatStagger,
            new BatStaggerState(_config, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.BatAttack,
            new BatAttackState(_config, enemyContext)
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
            new BatIdleState(_config, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.BatCharge,
            new BatChargeState(_config, enemyContext)
        );
    }
}
