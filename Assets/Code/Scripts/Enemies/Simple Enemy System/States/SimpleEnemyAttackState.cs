using UnityEngine;

public class SimpleEnemyAttackState : SimpleEnemyState
{
    public SimpleEnemyAttackState(SimpleEnemyBehavior enemyBehavior)
        : base(enemyBehavior) { }

    public override void EnterState()
    {
        _enemyBehavior.AttackEnter();
    }

    public override void ExitState()
    {
        _enemyBehavior.AttackExit();
    }

    public override void UpdateState()
    {
        _enemyBehavior.AttackUpdate();
    }

    public override void CheckSwitchState()
    {
        _enemyBehavior.AttackSwitchConditions();
    }
}
