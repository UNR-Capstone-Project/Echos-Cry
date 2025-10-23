using UnityEngine;

public class SimpleEnemyUnengagedState : SimpleEnemyState
{
    public SimpleEnemyUnengagedState(SimpleEnemyBehavior enemyBehavior)
        : base(enemyBehavior) { }

    public override void EnterState()
    {
        _enemyBehavior.UnengagedEnter();
    }

    public override void ExitState()
    {
        _enemyBehavior.UnengagedExit();
    }

    public override void UpdateState()
    {
        _enemyBehavior.UnengagedUpdate();
    }

    public override void CheckSwitchState()
    {
        _enemyBehavior.UnengagedSwitchConditions();
    }
}
