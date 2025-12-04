using System.Collections;
using UnityEngine;
using UnityEngine.AI;
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
    private float sqrMag;
    public RangeIdleState() 
    {
        sqrMag = Mathf.Pow(10f, 2);
    }
    public override void UpdateState02ms(SimpleEnemyManager enemyContext)
    {
        float distance = (PlayerRef.PlayerTransform.position - enemyContext.transform.position).sqrMagnitude;
        if (distance <= sqrMag) enemyContext.SwitchState(EnemyStates.RANGE_ROAM);
    }
}
public class RangeRoamState : SimpleEnemyState
{
    public RangeRoamState(){}
    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        float range = 6f;
        Vector3 point = Random.onUnitSphere * range;
        point.y = 0;
        Vector3 destination = PlayerRef.PlayerTransform.position + point;
        enemyContext.EnemyNMA.SetDestination(destination);
    }
    public override void UpdateState02ms(SimpleEnemyManager enemyContext)
    {
        NavMeshAgent agent = enemyContext.EnemyNMA;
        if (agent == null || !agent.isOnNavMesh) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) enemyContext.SwitchState(EnemyStates.RANGE_CHARGE);
        }
    }
}
public class RangeChargeAttackState : SimpleEnemyState
{
    public RangeChargeAttackState(){}
    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        enemyContext.StartCoroutine(ChargeDurationCoroutine(enemyContext));
    }
    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        enemyContext.StopAllCoroutines();
    }
    private IEnumerator ChargeDurationCoroutine(SimpleEnemyManager enemyContext)
    {
        yield return new WaitForSeconds(0.5f);
        enemyContext.SwitchState(EnemyStates.RANGE_ATTACK);
    }
}
public class RangeAttackState : SimpleEnemyState
{
    int count;
    public RangeAttackState(){}
    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        count = 0;
        enemyContext.EnemyBaseAttack.UseAttack();
        enemyContext.StartCoroutine(BetweenAttackPauseCoroutine(enemyContext));
    }
    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        enemyContext.StopAllCoroutines();
    }
    private IEnumerator AttackCooldownCoroutine(SimpleEnemyManager enemyContext)
    {
        yield return new WaitForSeconds(2f);
        enemyContext.SwitchState(EnemyStates.RANGE_ROAM);
    }
    private IEnumerator BetweenAttackPauseCoroutine(SimpleEnemyManager enemyContext)
    {
        yield return new WaitForSeconds(0.2f);
        if (count >= 2) enemyContext.StartCoroutine(AttackCooldownCoroutine(enemyContext));
        else
        {
            enemyContext.EnemyBaseAttack.UseAttack();
            count++;
            enemyContext.StartCoroutine(BetweenAttackPauseCoroutine(enemyContext));
        }
    }
}
public class RangeStaggerState : SimpleEnemyState
{
    public RangeStaggerState(){}
    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        if(enemyContext.EnemyNMA.hasPath) enemyContext.EnemyNMA.ResetPath();
        enemyContext.EnemyRigidbody.isKinematic = false;
        Vector3 direction = (PlayerRef.PlayerTransform.position - enemyContext.transform.position).normalized;
        enemyContext.EnemyRigidbody.AddForce(-(1f * direction), ForceMode.Impulse);
        enemyContext.StartCoroutine(StaggerDurationCoroutine(enemyContext));
    }
    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        enemyContext.EnemyRigidbody.isKinematic = true;
        enemyContext.StopAllCoroutines();
    }
    private IEnumerator StaggerDurationCoroutine(SimpleEnemyManager enemyContext)
    {
        yield return new WaitForSeconds(1f);
        enemyContext.SwitchState(EnemyStates.RANGE_ROAM);
    }
}
public class RangeDeathState : SimpleEnemyState
{
    public RangeDeathState(){}
    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        enemyContext.EnemyStats.HandleEnemyDeath();
    }
}
