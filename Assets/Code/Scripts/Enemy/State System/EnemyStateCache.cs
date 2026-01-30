using System.Collections.Generic;

public class EnemyStateCache 
{
    public enum EnemyStates
    {
        UNASSIGNED = 0,
        //Bat Enemy
        BatSpawn, BatStagger, BatDeath, BatCharge, BatAttack, BatIdle, BatChase,
        //Range Enemy
        RangeSpawn, RangeStagger, RangeDeath, RangeCharge, RangeAttack, RangeIdle, RangeRoam,

        WalkerSpawn, WalkerStagger, WalkerDeath, WalkerJump, WalkerAttack, WalkerIdle, WalkerChase
    }

    private readonly Dictionary<EnemyStates, EnemyState> _stateCache;
    private EnemyState _startStart;
    public EnemyState StartState { get => _startStart; set => _startStart = value; }

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

    public void Enable()
    {
        EnableStates();
    }
    public void Disable()
    {
        DisableStates();
    }
    private void EnableStates()
    {
        foreach (var state in _stateCache.Values)
            state.Enable();
    }
    private void DisableStates()
    {
        foreach (var state in _stateCache.Values)
            state.Disable();
    }
}