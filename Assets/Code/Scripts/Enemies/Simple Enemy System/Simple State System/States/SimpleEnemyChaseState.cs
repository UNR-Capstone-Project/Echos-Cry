using UnityEngine;

public class SimpleEnemyChaseState : SimpleEnemyState
{
    public SimpleEnemyChaseState(SimpleEnemyBehavior enemyBehavior)
        : base(enemyBehavior) { }

    public override void EnterState()
    {
        _enemyBehavior.ChaseEnter();
    }

    public override void ExitState()
    {
        _enemyBehavior.ChaseExit();
    }

    public override void UpdateState()
    {
        _enemyBehavior.ChaseUpdate();
    }

    public override void CheckSwitchState()
    {
        _enemyBehavior.ChaseSwitchConditions();
    }
}
