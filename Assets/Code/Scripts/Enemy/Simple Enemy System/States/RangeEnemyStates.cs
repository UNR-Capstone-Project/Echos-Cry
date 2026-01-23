using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static EnemyStateCache;

public class RangeSpawnState : EnemyState
{
    public RangeSpawnState(Enemy enemyContext) : base(enemyContext) { }

    protected override void OnEnter()
    {
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStates.RangeIdle));
    }
}

public class RangeIdleState : EnemyState
{
    private RangeEnemyConfigFile _config;

    public RangeIdleState(RangeEnemyConfigFile config, Enemy enemyContext) : base(enemyContext)
    {
        _config = config;
    }

    public override void Tick()
    {
        float distance = Vector3.Distance(PlayerRef.Transform.position, _enemyContext.transform.position);
        if (distance <= _config.DistanceCheck) 
            _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStates.RangeRoam));
    }
}
public class RangeRoamState : EnemyState
{
    public RangeRoamState(Enemy enemyContext) : base(enemyContext) { }

    protected override void OnEnter()
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
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) 
                _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStates.RangeCharge));
        }
    }
}
public class RangeChargeAttackState : EnemyState
{
    RangeEnemyConfigFile _config;
    public RangeChargeAttackState(RangeEnemyConfigFile config, Enemy enemyContext) : base(enemyContext) 
    { 
        _config = config;
    }

    protected override void OnEnter()
    {
        _enemyContext.StartCoroutine(ChargeDurationCoroutine());
    }
    protected override void OnExit()
    {
        _enemyContext.StopAllCoroutines();
    }
    private IEnumerator ChargeDurationCoroutine()
    {
        yield return new WaitForSeconds(_config.AttackChargeTime);
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStates.RangeAttack));
    }
}
public class RangeAttackState : EnemyState
{
    int count;
    RangeEnemyConfigFile _config;

    public RangeAttackState(RangeEnemyConfigFile config, Enemy enemyContext) : base(enemyContext) 
    { 
        _config = config;
    }

    protected override void OnEnter()
    {
        count = 0;
        //_enemyContext.EnemyBaseAttack.UseAttack();
        _enemyContext.StartCoroutine(BetweenAttackPauseCoroutine());
    }
    protected override void OnExit()
    {
        _enemyContext.StopAllCoroutines();
    }
    private IEnumerator AttackCooldownCoroutine()
    {
        yield return new WaitForSeconds(_config.AttackCooldown);
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStates.RangeRoam));
    }
    private IEnumerator BetweenAttackPauseCoroutine()
    {
        yield return new WaitForSeconds(TempoConductor.Instance.TimeBetweenBeats);
        if (count >= _config.ProjectileCount) _enemyContext.StartCoroutine(AttackCooldownCoroutine());
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
    RangeEnemyConfigFile _config;
    public RangeStaggerState(RangeEnemyConfigFile config, Enemy enemyContext) : base(enemyContext) 
    { 
        _config = config;
    }

    protected override void OnEnter()
    {
        if(_enemyContext.NavMeshAgent.hasPath) _enemyContext.NavMeshAgent.ResetPath();
        _enemyContext.Rigidbody.isKinematic = false;
        Vector3 direction = (PlayerRef.Transform.position - _enemyContext.transform.position).normalized;
        _enemyContext.Rigidbody.AddForce(-(_config.KnockbackForce * direction), ForceMode.Impulse);
        _enemyContext.StartCoroutine(StaggerDurationCoroutine());
    }
    protected override void OnExit()
    {
        _enemyContext.Rigidbody.isKinematic = true;
        _enemyContext.StopAllCoroutines();
    }
    private IEnumerator StaggerDurationCoroutine()
    {
        yield return new WaitForSeconds(_config.StaggerDuration);
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStates.RangeRoam));
    }
}
public class RangeDeathState : EnemyState
{
    public RangeDeathState(Enemy enemyContext) : base(enemyContext) { }

    protected override void OnEnter()
    {
        _enemyContext.DropsStrategy.Execute(_enemyContext.transform);
        Object.Destroy(_enemyContext.gameObject);
    }
}
