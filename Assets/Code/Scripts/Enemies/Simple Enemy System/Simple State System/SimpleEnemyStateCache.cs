//DO NOT ADJUST UNLESS STRICTLY NECESSARY
using System.Collections.Generic;

public class SimpleEnemyStateCache 
{
    public SimpleEnemyStateCache()
    {
        _stateCache = new Dictionary<States, SimpleEnemyState>
        {
            {States.BAT_SPAWN   , new BatSpawnState()        },
            {States.BAT_STAGGER , new BatStaggerState()      },
            {States.BAT_DEATH   , new BatDeathState()        },
            {States.BAT_CHARGE  , new BatChargeAttackState() },
            {States.BAT_ATTACK  , new BatAttackState()       },
            {States.BAT_IDLE    , new BatIdleState()         },
            {States.BAT_CHASE   , new BatChaseState()        }
        };
    }
    public static SimpleEnemyState RequestState(States requestedState)
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
    private static Dictionary<States, SimpleEnemyState> _stateCache;
}
