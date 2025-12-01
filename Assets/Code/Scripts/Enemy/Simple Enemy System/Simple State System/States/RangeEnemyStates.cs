using UnityEngine;
using static SimpleEnemyStateCache;

public class RangeSpawnState : SimpleEnemyState
{
    public RangeSpawnState(SimpleEnemyManager enemyContext) : base(enemyContext){}

    public override void EnterState()
    {
        enemyContext.SwitchState(States.RANGE_IDLE);
    }
}
public class RangeIdleState : SimpleEnemyState
{
    public RangeIdleState(SimpleEnemyManager enemyContext) : base(enemyContext) {}
}
public class RangeRoamState : SimpleEnemyState
{
    public RangeRoamState(SimpleEnemyManager enemyContext) : base(enemyContext)
    {
    }
}
public class RangeChargeAttackState : SimpleEnemyState
{
    public RangeChargeAttackState(SimpleEnemyManager enemyContext) : base(enemyContext)
    {
    }
}
public class RangeAttackState : SimpleEnemyState
{
    public RangeAttackState(SimpleEnemyManager enemyContext) : base(enemyContext)
    {
    }
}
public class RangeStaggerState : SimpleEnemyState
{
    public RangeStaggerState(SimpleEnemyManager enemyContext) : base(enemyContext)
    {
    }
}
public class RangeDeathState : SimpleEnemyState
{
    public RangeDeathState(SimpleEnemyManager enemyContext) : base(enemyContext)
    {
    }
}
