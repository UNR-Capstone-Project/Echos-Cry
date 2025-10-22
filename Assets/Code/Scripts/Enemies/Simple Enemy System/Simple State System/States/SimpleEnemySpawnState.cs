using UnityEngine;

public class SimpleEnemySpawnState : SimpleEnemyState
{
    public SimpleEnemySpawnState(SimpleEnemyBehavior enemyBehavior)
        : base(enemyBehavior) { }

    public override void EnterState()
    {
        _enemyBehavior.SpawnEnter();
    }

    public override void ExitState()
    {
        _enemyBehavior.SpawnExit();
    }

    public override void UpdateState()
    {
        _enemyBehavior.SpawnUpdate();
    }

    public override void CheckSwitchState()
    {
        _enemyBehavior.SpawnSwitchConditions();
    }
}
