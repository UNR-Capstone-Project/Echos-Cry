//DO NOT ADJUST UNLESS STRICTLY NECESSARY
using System.Collections.Generic;

public class SimpleEnemyStateCache 
{
    public SimpleEnemyStateCache()
    {
        _stateCache = new Dictionary<EnemyStates, SimpleEnemyState>();

        _stateCache.Add(EnemyStates.BAT_SPAWN   , new BatSpawnState());
        _stateCache.Add(EnemyStates.BAT_STAGGER , new BatStaggerState());
        _stateCache.Add(EnemyStates.BAT_DEATH   , new BatDeathState());
        _stateCache.Add(EnemyStates.BAT_CHARGE  , new BatChargeAttackState());
        _stateCache.Add(EnemyStates.BAT_ATTACK  , new BatAttackState());
        _stateCache.Add(EnemyStates.BAT_IDLE    , new BatIdleState());
        _stateCache.Add(EnemyStates.BAT_CHASE   , new BatChaseState());

        _stateCache.Add(EnemyStates.RANGE_SPAWN   , new RangeSpawnState());
        _stateCache.Add(EnemyStates.RANGE_STAGGER , new RangeStaggerState());
        _stateCache.Add(EnemyStates.RANGE_DEATH   , new RangeDeathState());
        _stateCache.Add(EnemyStates.RANGE_CHARGE  , new RangeChargeAttackState());
        _stateCache.Add(EnemyStates.RANGE_ATTACK  , new RangeAttackState());
        _stateCache.Add(EnemyStates.RANGE_IDLE    , new RangeIdleState());
        _stateCache.Add(EnemyStates.RANGE_ROAM    , new RangeRoamState());

        _stateCache.Add(EnemyStates.WALKER_SPAWN, new WalkerSpawnState());
        _stateCache.Add(EnemyStates.WALKER_STAGGER, new WalkerStaggerState());
        _stateCache.Add(EnemyStates.WALKER_DEATH, new WalkerDeathState());
        _stateCache.Add(EnemyStates.WALKER_CHARGE, new WalkerChargeAttackState());
        _stateCache.Add(EnemyStates.WALKER_ATTACK, new WalkerAttackState());
        _stateCache.Add(EnemyStates.WALKER_IDLE, new WalkerIdleState());
        _stateCache.Add(EnemyStates.WALKER_CHASE, new WalkerChaseState());
    }

    public SimpleEnemyState RequestState(EnemyStates requestedState)
    {
        if (!_stateCache.ContainsKey(requestedState)) return null;
        else return _stateCache[requestedState];
    }

    public enum EnemyStates
    {
        UNASSIGNED = 0,
        //Bat Enemy
        BAT_SPAWN, BAT_STAGGER, BAT_DEATH, BAT_CHARGE, BAT_ATTACK, BAT_IDLE, BAT_CHASE,
        //Range Enemy
        RANGE_SPAWN, RANGE_STAGGER, RANGE_DEATH, RANGE_CHARGE, RANGE_ATTACK, RANGE_IDLE, RANGE_ROAM,
        //Walker Enemy
        WALKER_SPAWN, WALKER_STAGGER, WALKER_DEATH, WALKER_CHARGE, WALKER_ATTACK, WALKER_IDLE, WALKER_CHASE,
    }

    protected Dictionary<EnemyStates, SimpleEnemyState> _stateCache;
}

//public class BatEnemyStateCache : SimpleEnemyStateCache
//{
//    public BatEnemyStateCache(SimpleEnemyManager enemyContext) : base(enemyContext)
//    {
//        _stateCache.Add(States.BAT_SPAWN, new BatSpawnState(enemyContext));
//        _stateCache.Add(States.BAT_STAGGER, new BatStaggerState(enemyContext));
//        _stateCache.Add(States.BAT_DEATH, new BatDeathState(enemyContext));
//        _stateCache.Add(States.BAT_CHARGE, new BatChargeAttackState(enemyContext));
//        _stateCache.Add(States.BAT_ATTACK, new BatAttackState(enemyContext));
//        _stateCache.Add(States.BAT_IDLE, new BatIdleState(enemyContext));
//        _stateCache.Add(States.BAT_CHASE, new BatChaseState(enemyContext));
//    }
//}

//public class RangeEnemyStateCache : SimpleEnemyStateCache
//{
//    public RangeEnemyStateCache(SimpleEnemyManager enemyContext) : base(enemyContext)
//    {
//        _stateCache.Add(States.RANGE_SPAWN, new RangeSpawnState(enemyContext));
//        _stateCache.Add(States.RANGE_STAGGER, new RangeStaggerState(enemyContext));
//        _stateCache.Add(States.RANGE_DEATH, new RangeDeathState(enemyContext));
//        _stateCache.Add(States.RANGE_CHARGE, new RangeChargeAttackState(enemyContext));
//        _stateCache.Add(States.RANGE_ATTACK, new RangeAttackState(enemyContext));
//        _stateCache.Add(States.RANGE_IDLE, new RangeIdleState(enemyContext));
//        _stateCache.Add(States.RANGE_ROAM, new RangeRoamState(enemyContext));
//    }
//}
