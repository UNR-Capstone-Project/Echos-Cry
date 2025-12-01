//DO NOT ADJUST UNLESS STRICTLY NECESSARY
using System.Collections.Generic;

public abstract class SimpleEnemyStateCache 
{
    public SimpleEnemyStateCache(SimpleEnemyManager enemyContext)
    {
        _stateCache = new Dictionary<States, SimpleEnemyState>();
    }

    public SimpleEnemyState RequestState(States requestedState)
    {
        if (!_stateCache.ContainsKey(requestedState)) return null;
        else return _stateCache[requestedState];
    }

    public enum States
    {
        UNASSIGNED = 0,
        //Bat Enemy
        BAT_SPAWN, BAT_STAGGER, BAT_DEATH, BAT_CHARGE, BAT_ATTACK, BAT_IDLE, BAT_CHASE,
        //Range Enemy
        RANGE_SPAWN, RANGE_STAGGER, RANGE_DEATH, RANGE_CHARGE, RANGE_ATTACK, RANGE_IDLE, RANGE_ROAM
    }

    protected Dictionary<States, SimpleEnemyState> _stateCache;
}

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

public class RangeEnemyStateCache : SimpleEnemyStateCache
{
    public RangeEnemyStateCache(SimpleEnemyManager enemyContext) : base(enemyContext)
    {
        _stateCache.Add(States.RANGE_SPAWN, new RangeSpawnState(enemyContext));
        _stateCache.Add(States.RANGE_STAGGER, new RangeStaggerState(enemyContext));
        _stateCache.Add(States.RANGE_DEATH, new RangeDeathState(enemyContext));
        _stateCache.Add(States.RANGE_CHARGE, new RangeChargeAttackState(enemyContext));
        _stateCache.Add(States.RANGE_ATTACK, new RangeAttackState(enemyContext));
        _stateCache.Add(States.RANGE_IDLE, new RangeIdleState(enemyContext));
        _stateCache.Add(States.RANGE_ROAM, new RangeRoamState(enemyContext));
    }
}
