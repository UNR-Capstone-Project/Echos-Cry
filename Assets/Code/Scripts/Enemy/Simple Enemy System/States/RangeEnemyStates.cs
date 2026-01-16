using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static EnemyStateCache;

public class RangeSpawnState : EnemyState
{
    public RangeSpawnState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache) { }

    public override void Enter()
    {
        _enemyStateMachine.SwitchState(_enemyStateCache.RequestState(EnemyStates.Range_Idle));
    }
}

public class RangeIdleState : EnemyState
{
    private float sqrMag;

    public RangeIdleState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache)
    {
        sqrMag = Mathf.Pow(10f, 2);
    }

    public override void UpdateState02ms(Enemy enemyContext)
    {
        float distance = (PlayerRef.PlayerTransform.position - enemyContext.transform.position).sqrMagnitude;
        if (distance <= sqrMag) _enemyStateMachine.SwitchState(_enemyStateCache.RequestState(EnemyStates.Range_Roam));
    }
}
public class RangeRoamState : EnemyState
{
    public RangeRoamState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache) { }

    public override void Enter()
    {
        float range = 6f;
        Vector3 point = Random.onUnitSphere * range;
        point.y = 0;
        Vector3 destination = PlayerRef.PlayerTransform.position + point;
        _enemyContext.EnemyNMA.SetDestination(destination);
    }
    public override void UpdateState02ms(Enemy enemyContext)
    {
        NavMeshAgent agent = enemyContext.EnemyNMA;
        if (agent == null || !agent.isOnNavMesh) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) _enemyStateMachine.SwitchState(_enemyStateCache.RequestState(EnemyStates.Range_Charge));
        }
    }
}
public class RangeChargeAttackState : EnemyState
{
    public RangeChargeAttackState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache) { }

    public override void Enter()
    {
        _enemyContext.StartCoroutine(ChargeDurationCoroutine());
    }
    public override void Exit()
    {
        _enemyContext.StopAllCoroutines();
    }
    private IEnumerator ChargeDurationCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        _enemyStateMachine.SwitchState(_enemyStateCache.RequestState(EnemyStates.Range_Attack));
    }
}
public class RangeAttackState : EnemyState
{
    int count;

    public RangeAttackState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache) { }

    public override void Enter()
    {
        count = 0;
        _enemyContext.EnemyBaseAttack.UseAttack();
        _enemyContext.StartCoroutine(BetweenAttackPauseCoroutine());
    }
    public override void Exit()
    {
        _enemyContext.StopAllCoroutines();
    }
    private IEnumerator AttackCooldownCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _enemyStateMachine.SwitchState(_enemyStateCache.RequestState(EnemyStates.Range_Roam));
    }
    private IEnumerator BetweenAttackPauseCoroutine()
    {
        yield return new WaitForSeconds(TempoManager.TimeBetweenBeats);
        if (count >= 2) _enemyContext.StartCoroutine(AttackCooldownCoroutine());
        else
        {
            _enemyContext.EnemyBaseAttack.UseAttack();
            count++;
            _enemyContext.StartCoroutine(BetweenAttackPauseCoroutine());
        }
    }
}
public class RangeStaggerState : EnemyState
{
    public RangeStaggerState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache) { }

    public override void Enter()
    {
        if(_enemyContext.EnemyNMA.hasPath) _enemyContext.EnemyNMA.ResetPath();
        _enemyContext.EnemyRigidbody.isKinematic = false;
        Vector3 direction = (PlayerRef.PlayerTransform.position - _enemyContext.transform.position).normalized;
        _enemyContext.EnemyRigidbody.AddForce(-(1f * direction), ForceMode.Impulse);
        _enemyContext.StartCoroutine(StaggerDurationCoroutine());
    }
    public override void Exit()
    {
        _enemyContext.EnemyRigidbody.isKinematic = true;
        _enemyContext.StopAllCoroutines();
    }
    private IEnumerator StaggerDurationCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _enemyStateMachine.SwitchState(_enemyStateCache.RequestState(EnemyStates.Range_Roam));
    }
}
public class RangeDeathState : EnemyState
{
    public RangeDeathState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache) { }

    public override void Enter()
    {
        _enemyContext.EnemyStats.HandleEnemyDeath();
    }
}
