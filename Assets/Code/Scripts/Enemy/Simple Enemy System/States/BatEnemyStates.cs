using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static EnemyStateCache;

//New Implementation of enemy behaviors
//Same as previous system but allows for more customizability for enemy states

//Instead of implementing logic for enemies in hard-coded functions that already exist and prevent customizability for new states,
//This system will just allow you to implement individual states yourself

//Keep enemy states for one enemy in a single script file as shown below

//USE THIS SCRIPT AS A REFERENCE AND GUIDE ON HOW IT WORKS

//NOTE: CheckSwitchState is called automatically in the StateMachine so dont worry about calling it individually

public class BatSpawnState : EnemyState
{
    public BatSpawnState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache) { }

    public override void Enter()
    {
        _enemyStateMachine.SwitchState(_enemyStateCache.RequestState(EnemyStates.BAT_IDLE));
    }
}

public class BatIdleState : EnemyState
{
    private float sqrMagDistance;

    public BatIdleState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache)
    {
        sqrMagDistance = Mathf.Pow(10f, 2f);
    }

    public override void UpdateState02ms(EnemyManager enemyContext)
    {
        CheckPlayerDistance(enemyContext);
    }
    public  void CheckPlayerDistance(EnemyManager enemyContext)
    {
        float playerDistance = (enemyContext.transform.position - PlayerRef.PlayerTransform.position).sqrMagnitude;
        if (playerDistance < sqrMagDistance)
        {
            _enemyStateMachine.SwitchState(_enemyStateCache.RequestState(EnemyStates.BAT_CHASE));
            return;
        }
    }
}

public class BatChaseState : EnemyState
{
    public BatChaseState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache) { }

    public override void Enter()
    {
        SetEnemyTarget();
    }
    public override void Exit()
    {
        _enemyContext.StopAllCoroutines();
    }
    public override void UpdateState02ms(EnemyManager enemyContext)
    {
        CheckNavMeshDistance();
        SetEnemyTarget();
    }

    private void CheckNavMeshDistance()
    {
        NavMeshAgent agent = _enemyContext.EnemyNMA;
        if (agent == null) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) 
                _enemyStateMachine.SwitchState(_enemyStateCache.RequestState(EnemyStates.BAT_CHARGE));
        }
    }
    private void SetEnemyTarget()
    {
        if(_enemyContext.EnemyNMA == null) return;
        _enemyContext.EnemyNMA.SetDestination(PlayerRef.PlayerTransform.position);
    }
}

public class BatChargeAttackState : EnemyState
{
    private float chargeDuration;

    public BatChargeAttackState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache)
    {
        chargeDuration = 1f;
    }

    public override void Enter()
    {
        //Debug.Log("Enter Charge Attack State");
        _enemyContext.StartCoroutine(ChargeAttackCoroutine());
    }
    public override void Exit()
    {
        //Debug.Log("Exit Charge Attack State");
        _enemyContext.StopAllCoroutines();
    }

    IEnumerator ChargeAttackCoroutine()
    {
        yield return new WaitForSeconds(chargeDuration);
        if (TempoManager.CurrentHitQuality != TempoManager.HIT_QUALITY.MISS) 
            _enemyStateMachine.SwitchState(_enemyStateCache.RequestState(EnemyStates.BAT_ATTACK));
        else _enemyContext.StartCoroutine(WaitUntilBeat());
    }
    IEnumerator WaitUntilBeat()
    {
        yield return new WaitForEndOfFrame();
        if (TempoManager.CurrentHitQuality != TempoManager.HIT_QUALITY.MISS) 
            _enemyStateMachine.SwitchState(_enemyStateCache.RequestState(EnemyStates.BAT_ATTACK));
        else _enemyContext.StartCoroutine(WaitUntilBeat());
    }
}

public class BatAttackState : EnemyState
{
    public BatAttackState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache) { }

    public override void Enter()
    {
        _enemyContext.EnemyBaseAttack.UseAttack();
    }
}
public class BatStaggerState : EnemyState
{
    private float staggerDuration;
    private float knockbackForce;

    public BatStaggerState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache)
    {
        staggerDuration = 1f;
        knockbackForce = 0.5f;
    }

    public override void Enter()
    {
        //Debug.Log("Enter Stagger State");
        _enemyContext.EnemyRigidbody.isKinematic = false;
        Vector3 direction = (PlayerRef.PlayerTransform.position - _enemyContext.transform.position).normalized;
        _enemyContext.EnemyRigidbody.AddForce(-(knockbackForce * direction), ForceMode.Impulse);
        _enemyContext.StartCoroutine(StaggerDuration());
    }
    public override void Exit()
    {
        _enemyContext.EnemyRigidbody.isKinematic = true;
        _enemyContext.StopAllCoroutines();
    }

    private IEnumerator StaggerDuration()
    {
        yield return new WaitForSeconds(staggerDuration);
        _enemyStateMachine.SwitchState(_enemyStateCache.RequestState(EnemyStates.BAT_CHASE));
    }
}
public class BatDeathState : EnemyState
{
    public BatDeathState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache) : base(enemyStateMachine, enemyStateCache) { }

    public override void Enter()
    {
        //Debug.Log("Enter Death State");
        _enemyContext.EnemyStats.HandleEnemyDeath();
    }
}