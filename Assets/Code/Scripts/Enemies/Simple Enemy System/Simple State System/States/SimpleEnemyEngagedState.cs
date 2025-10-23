using UnityEngine;

public class SimpleEnemyEngagedState : SimpleEnemyState
{
    public SimpleEnemyEngagedState(SimpleEnemyBehavior enemyBehavior)
        : base(enemyBehavior) { }

    public override void EnterState()
    {
        _enemyBehavior.EngagedEnter();
    }

    public override void ExitState()
    {
        _enemyBehavior.EngagedExit();
    }

    public override void UpdateState()
    {
        _enemyBehavior.EngagedUpdate();
    }

    public override void CheckSwitchState()
    {
        _enemyBehavior.EngagedSwitchConditions();
    }
}
