using UnityEngine;

public class SimpleEnemyIdleState : SimpleEnemyState
{
    public SimpleEnemyIdleState(SimpleEnemyBehavior enemyBehavior)
        : base(enemyBehavior) { }

    public override void EnterState()
    {
        _enemyBehavior.IdleEnter();
    }

    public override void ExitState()
    {
        _enemyBehavior.IdleExit();
    }

    public override void UpdateState()
    {
        _enemyBehavior.IdleUpdate();
    }

    public override void CheckSwitchState()
    {
        _enemyBehavior.IdleSwitchConditions();
    }
}
