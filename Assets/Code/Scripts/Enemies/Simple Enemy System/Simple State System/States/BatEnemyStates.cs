using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static SimpleEnemyStateCache;

public class BatSpawnState : SimpleEnemyState
{
    public BatSpawnState() { }

    public override void CheckSwitchState(SimpleEnemyManager enemyContext)
    {
        base.CheckSwitchState(enemyContext);
    }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_IDLE));
    }

    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        base.ExitState(enemyContext);
    }

    public override void UpdateState(SimpleEnemyManager enemyContext)
    {
        base.UpdateState(enemyContext);
    }
}

public class BatIdleState : SimpleEnemyState
{
    public BatIdleState() { }

    public override void CheckSwitchState(SimpleEnemyManager enemyContext)
    {
        float playerDistance = (enemyContext.transform.position - PlayerRef.PlayerTransform.position).sqrMagnitude;
        if (playerDistance < 25f)
        {
            enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_CHASE));
        }
    }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        base.EnterState(enemyContext);
    }

    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        base.ExitState(enemyContext);
    }

    public override void UpdateState(SimpleEnemyManager enemyContext)
    {
        base.UpdateState(enemyContext);
    }
}

public class BatChaseState : SimpleEnemyState
{
    public BatChaseState() { }

    public override void CheckSwitchState(SimpleEnemyManager enemyContext)
    {
        NavMeshAgent agent = enemyContext.EnemyNMA;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_CHARGE));
        }
    }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        base.EnterState(enemyContext);
    }

    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        base.ExitState(enemyContext);
    }

    public override void UpdateState(SimpleEnemyManager enemyContext)
    {
        enemyContext.EnemyNMA.SetDestination(PlayerRef.PlayerTransform.position);
    }
}

public class BatChargeAttackState : SimpleEnemyState
{
    public BatChargeAttackState() { }

    public override void CheckSwitchState(SimpleEnemyManager enemyContext)
    {
        base.CheckSwitchState(enemyContext);
    }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        base.EnterState(enemyContext);
    }

    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        base.ExitState(enemyContext);
    }

    public override void UpdateState(SimpleEnemyManager enemyContext)
    {
        base.UpdateState(enemyContext);
    }
    IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(5f);
    }
}

public class BatAttackState : SimpleEnemyState
{
    public BatAttackState() { }

    public override void CheckSwitchState(SimpleEnemyManager enemyContext)
    {
        base.CheckSwitchState(enemyContext);
    }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        base.EnterState(enemyContext);
    }

    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        base.ExitState(enemyContext);
    }

    public override void UpdateState(SimpleEnemyManager enemyContext)
    {
        base.UpdateState(enemyContext);
    }
}
public class BatStaggerState : SimpleEnemyState
{
    public BatStaggerState() { }

    public override void CheckSwitchState(SimpleEnemyManager enemyContext)
    {
        base.CheckSwitchState(enemyContext  );
    }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        base.EnterState(enemyContext);
    }

    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        base.ExitState(enemyContext);
    }

    public override void UpdateState(SimpleEnemyManager enemyContext)
    {
        base.UpdateState(enemyContext);
    }
}
public class BatDeathState : SimpleEnemyState
{
    public BatDeathState() { }

    public override void CheckSwitchState(SimpleEnemyManager enemyContext)
    {
        base.CheckSwitchState(enemyContext);
    }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        base.EnterState(enemyContext);
    }

    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        base.ExitState(enemyContext);
    }

    public override void UpdateState(SimpleEnemyManager enemyContext)
    {
        base.UpdateState(enemyContext);
    }
}
