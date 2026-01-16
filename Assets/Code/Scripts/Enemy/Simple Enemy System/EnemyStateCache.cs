using System.Collections.Generic;

public class EnemyStateCache 
{
    public enum EnemyStates
    {
        UNASSIGNED = 0,
        //Bat Enemy
        Bat_Spawn, Bat_Stagger, Bat_Death, Bat_Charge, Bat_Attack, Bat_Idle, Bat_Chase,
        //Range Enemy
        Range_Spawn, Range_Stagger, Range_Death, Range_Charge, Range_Attack, Range_Idle, Range_Roam
    }
    private readonly Dictionary<EnemyStates, EnemyState> _stateCache;

    public EnemyStateCache()
    {
        _stateCache = new();
    }

    public EnemyState RequestState(EnemyStates requestedState)
    {
        if (!_stateCache.ContainsKey(requestedState)) return null;
        else return _stateCache[requestedState];
    }
    public void AddState(EnemyStates statesEnum, EnemyState state)
    {
        _stateCache.Add(statesEnum, state);
    }

}