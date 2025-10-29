using UnityEngine;

public class SimpleEnemyInitiateState : SimpleEnemyState
{
    public SimpleEnemyInitiateState(SimpleEnemyBehavior enemyBehavior)
        : base(enemyBehavior) { }

    public override void EnterState()
    {
        _enemyBehavior.InitiateEnter();
    }

    public override void ExitState()
    {
        _enemyBehavior.InitiateExit();
    }

    public override void UpdateState()
    {
        _enemyBehavior.InitiateUpdate();
    }

    public override void CheckSwitchState()
    {
        _enemyBehavior.InitiateSwitchConditions();
    }
}
