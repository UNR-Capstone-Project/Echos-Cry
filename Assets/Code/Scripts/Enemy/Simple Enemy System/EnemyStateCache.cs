using System.Collections.Generic;

public class EnemyStateCache 
{
    public enum EnemyStates
    {
        UNASSIGNED = 0,
        //Bat Enemy
        Bat_Spawn, BAT_STAGGER, BAT_DEATH, BAT_CHARGE, BAT_ATTACK, BAT_IDLE, BAT_CHASE,
        //Range Enemy
        RANGE_SPAWN, RANGE_STAGGER, RANGE_DEATH, RANGE_CHARGE, RANGE_ATTACK, RANGE_IDLE, RANGE_ROAM
    }
    private Dictionary<EnemyStates, EnemyState> _stateCache;
    public Dictionary<EnemyStates, EnemyState> StateCache { get { return _stateCache; } }

    public EnemyStateCache()
    {
        _stateCache = new();
    }

    public EnemyState RequestState(EnemyStates requestedState)
    {
        if (!_stateCache.ContainsKey(requestedState)) return null;
        else return _stateCache[requestedState];
    }

}