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
        BAT_SPAWN, BAT_STAGGER, BAT_DEATH, BAT_CHARGE, BAT_ATTACK, BAT_IDLE, BAT_CHASE
    }
    protected Dictionary<States, SimpleEnemyState> _stateCache;
}
