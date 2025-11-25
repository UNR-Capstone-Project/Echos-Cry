using UnityEngine;

public class BatEnemyStateCache : SimpleEnemyStateCache
{
    public BatEnemyStateCache(SimpleEnemyManager enemyContext) : base(enemyContext)
    {
        _stateCache.Add(States.BAT_SPAWN, new BatSpawnState(enemyContext));
        _stateCache.Add(States.BAT_STAGGER, new BatStaggerState(enemyContext));
        _stateCache.Add(States.BAT_DEATH, new BatDeathState(enemyContext));
        _stateCache.Add(States.BAT_CHARGE, new BatChargeAttackState(enemyContext));
        _stateCache.Add(States.BAT_ATTACK, new BatAttackState(enemyContext));
        _stateCache.Add(States.BAT_IDLE, new BatIdleState(enemyContext));
        _stateCache.Add(States.BAT_CHASE, new BatChaseState(enemyContext));
    }
}
