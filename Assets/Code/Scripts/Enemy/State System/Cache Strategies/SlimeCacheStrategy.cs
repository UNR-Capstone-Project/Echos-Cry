using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Enemy/Cache Strategy/Slime")]
public class SlimeCacheStrategy : EnemyCacheStrategy
{
    [SerializeField] private SlimeData _data;

    public override void Execute(EnemyStateCache stateCache, Enemy enemyContext)
    {
        stateCache.AddState(
            EnemyStateCache.EnemyStates.SlimeSpawn,
            new SlimeSpawnState(enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.SlimeStagger,
            new SlimeStaggerState(_data, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.SlimeAttack,
            new SlimeAttackState(_data, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.SlimeChase,
            new SlimeChaseState(enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.SlimeDeath,
            new SlimeDeathState(enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.SlimeIdle,
            new SlimeIdleState(_data, enemyContext)
        );
        stateCache.AddState(
            EnemyStateCache.EnemyStates.SlimeCharge,
            new SlimeChargeState(_data, enemyContext)
        );
        stateCache.StartState = stateCache.RequestState(EnemyStateCache.EnemyStates.SlimeSpawn);
    }
}
