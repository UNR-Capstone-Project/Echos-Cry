using UnityEngine;

public class SimpleEnemyStaggerState : SimpleEnemyState
{
    public SimpleEnemyStaggerState(SimpleEnemyBehavior enemyBehavior)
        : base(enemyBehavior) { }

    public override void EnterState()
    {
        _enemyBehavior.StaggerEnter();
    }

    public override void ExitState()
    {
        _enemyBehavior.StaggerExit();
    }

    public override void UpdateState()
    {
        _enemyBehavior.StaggerUpdate();
    }

    public override void CheckSwitchState()
    {
        _enemyBehavior.StaggerSwitchConditions();
    }
}
