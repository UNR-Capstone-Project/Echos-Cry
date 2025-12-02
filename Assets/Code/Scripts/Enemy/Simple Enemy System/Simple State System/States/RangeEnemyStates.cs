using UnityEngine;
using static SimpleEnemyStateCache;

public class RangeSpawnState : SimpleEnemyState
{
    public RangeSpawnState() { }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        enemyContext.SwitchState(EnemyStates.RANGE_IDLE);
    }
}
public class RangeIdleState : SimpleEnemyState
{
    public RangeIdleState() {}
}
public class RangeRoamState : SimpleEnemyState
{
    public RangeRoamState()
    {
    }
}
public class RangeChargeAttackState : SimpleEnemyState
{
    public RangeChargeAttackState()
    {
    }
}
public class RangeAttackState : SimpleEnemyState
{
    public RangeAttackState()
    {
    }
}
public class RangeStaggerState : SimpleEnemyState
{
    public RangeStaggerState()
    {
    }
}
public class RangeDeathState : SimpleEnemyState
{
    public RangeDeathState()
    {
    }
}
