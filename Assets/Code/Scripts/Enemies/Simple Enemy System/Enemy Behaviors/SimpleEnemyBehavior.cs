
using System;

public abstract class SimpleEnemyBehavior 
{
    protected SimpleEnemyManager _seManager;
    protected SimpleEnemyStateCache _stateCache;
    protected SimpleEnemyStateMachine _stateMachine;
    protected event Action<SimpleEnemyState> SwitchStateEvent;

    public SimpleEnemyBehavior(SimpleEnemyManager seManager, SimpleEnemyStateCache stateCache, SimpleEnemyStateMachine stateMachine)
    {
        _seManager = seManager;
        _stateCache = stateCache;
        _stateMachine = stateMachine;

        SwitchStateEvent += _stateMachine.HandleSwitchState;
    }

    public abstract void ChaseUpdate();
    public abstract void ChaseEnter();
    public abstract void ChaseExit();
    public abstract void ChaseSwitchConditions();

    public abstract void AttackUpdate();
    public abstract void AttackEnter();
    public abstract void AttackExit();
    public abstract void AttackSwitchConditions();

    public abstract void SpawnUpdate();
    public abstract void SpawnEnter();
    public abstract void SpawnExit();
    public abstract void SpawnSwitchConditions();

    public abstract void StaggerUpdate();
    public abstract void StaggerEnter();
    public abstract void StaggerExit();
    public abstract void StaggerSwitchConditions();

    public abstract void IdleUpdate();
    public abstract void IdleEnter();
    public abstract void IdleExit();
    public abstract void IdleSwitchConditions();
}
