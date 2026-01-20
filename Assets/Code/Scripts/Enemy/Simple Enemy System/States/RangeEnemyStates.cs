using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static EnemyStateCache;

public class RangeSpawnState : EnemyState
{
    public RangeSpawnState(Enemy enemyContext) : base(enemyContext) { }

    public override void Enter()
    {
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStates.Range_Idle));
    }
}

public class RangeIdleState : EnemyState
{
    private float sqrMag;

    public RangeIdleState(Enemy enemyContext) : base(enemyContext)
    {
        sqrMag = Mathf.Pow(10f, 2);
    }

    public override void Tick()
    {
        float distance = (PlayerRef.Transform.position - _enemyContext.transform.position).sqrMagnitude;
        if (distance <= sqrMag) _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStates.Range_Roam));
    }
}
public class RangeRoamState : EnemyState
{
    public RangeRoamState(Enemy enemyContext) : base(enemyContext) { }

    public override void Enter()
    {
        float range = 6f;
        Vector3 point = Random.onUnitSphere * range;
        point.y = 0;
        Vector3 destination = PlayerRef.Transform.position + point;
        _enemyContext.NavMeshAgent.SetDestination(destination);
    }
    public override void Tick()
    {
        NavMeshAgent agent = _enemyContext.NavMeshAgent;
        if (agent == null || !agent.isOnNavMesh) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStates.Range_Charge));
        }
    }
}
public class RangeChargeAttackState : EnemyState
{
    public RangeChargeAttackState(Enemy enemyContext) : base(enemyContext) { }

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
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStates.Range_Attack));
    }
}
public class RangeAttackState : EnemyState
{
    int count;

    public RangeAttackState(Enemy enemyContext) : base(enemyContext) { }

    public override void Enter()
    {
        count = 0;
        //_enemyContext.EnemyBaseAttack.UseAttack();
        _enemyContext.StartCoroutine(BetweenAttackPauseCoroutine());
    }
    public override void Exit()
    {
        _enemyContext.StopAllCoroutines();
    }
    private IEnumerator AttackCooldownCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStates.Range_Roam));
    }
    private IEnumerator BetweenAttackPauseCoroutine()
    {
        yield return new WaitForSeconds(TempoConductor.Instance.TimeBetweenBeats);
        if (count >= 2) _enemyContext.StartCoroutine(AttackCooldownCoroutine());
        else
        {
            //_enemyContext.EnemyBaseAttack.UseAttack();
            count++;
            _enemyContext.StartCoroutine(BetweenAttackPauseCoroutine());
        }
    }
}
public class RangeStaggerState : EnemyState
{
    public RangeStaggerState(Enemy enemyContext) : base(enemyContext) { }

    public override void Enter()
    {
        if(_enemyContext.NavMeshAgent.hasPath) _enemyContext.NavMeshAgent.ResetPath();
        _enemyContext.Rigidbody.isKinematic = false;
        Vector3 direction = (PlayerRef.Transform.position - _enemyContext.transform.position).normalized;
        _enemyContext.Rigidbody.AddForce(-(1f * direction), ForceMode.Impulse);
        _enemyContext.StartCoroutine(StaggerDurationCoroutine());
    }
    public override void Exit()
    {
        _enemyContext.Rigidbody.isKinematic = true;
        _enemyContext.StopAllCoroutines();
    }
    private IEnumerator StaggerDurationCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStates.Range_Roam));
    }
}
public class RangeDeathState : EnemyState
{
    public RangeDeathState(Enemy enemyContext) : base(enemyContext) { }

    public override void Enter()
    {
        //_enemyContext.Stats.HandleEnemyDeath();
    }
}
