using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

//New Implementation of enemy behaviors
//Same as previous system but allows for more customizability for enemy states

//Instead of implementing logic for enemies in hard-coded functions that already exist and prevent customizability for new states,
//This system will just allow you to implement individual states yourself

//Keep enemy states for one enemy in a single script file as shown below

//USE THIS SCRIPT AS A REFERENCE AND GUIDE ON HOW IT WORKS

//NOTE: CheckSwitchState is called automatically in the StateMachine so dont worry about calling it individually

public class WalkerSpawnState : EnemyState
{
    public WalkerSpawnState(Enemy enemyContext) : base(enemyContext) { }

    protected override void OnEnter()
    {
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerIdle));
    }
}

public class WalkerIdleState : EnemyState
{
    private float sqrMagDistance;

    public WalkerIdleState(Enemy enemyContext) : base(enemyContext)
    {
        sqrMagDistance = Mathf.Pow(10f, 2f);
    }

    protected override void OnEnter()
    {
        _enemyContext.Animator.SetBool("isWalking", false);
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerDeath));
    }

    public override void Tick()
    {
        CheckPlayerDistance();
    }
    public  void CheckPlayerDistance()
    {
        float playerDistance = (_enemyContext.transform.position - PlayerRef.Transform.position).sqrMagnitude;
        if (playerDistance < sqrMagDistance)
        {
            _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerChase));
            return;
        }
    }
}

public class WalkerChaseState : EnemyState
{
    public WalkerChaseState(Enemy enemyContext) : base(enemyContext) { }

    protected override void OnEnter()
    {
        _enemyContext.Animator.SetBool("isWalking", true);
        SetEnemyTarget();
    }
    protected override void OnExit()
    {
        _enemyContext.StopAllCoroutines();
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerDeath));
    }
    public override void Tick()
    {
        CheckNavMeshDistance();
        SetEnemyTarget();
    }

    private void CheckNavMeshDistance()
    {
        NavMeshAgent agent = _enemyContext.NavMeshAgent;
        if (agent == null) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) 
                _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerCharge));
        }
    }
    private void SetEnemyTarget()
    {
        if(_enemyContext.NavMeshAgent == null) return;
        _enemyContext.NavMeshAgent.SetDestination(PlayerRef.Transform.position);
    }
}

public class WalkerChargeAttackState : EnemyState
{
    private float chargeDuration;

    public WalkerChargeAttackState(Enemy enemyContext) : base(enemyContext)
    {
        chargeDuration = 0.8f;
    }

    protected override void OnEnter()
    {
        //Debug.Log("Enter Charge Attack State");
        _enemyContext.Animator.SetBool("isWalking", false);
        _enemyContext.StartCoroutine(ChargeAttackCoroutine());
    }
    protected override void OnExit()
    {
        //Debug.Log("Exit Charge Attack State");
        _enemyContext.StopAllCoroutines();
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerDeath));
    }

    IEnumerator ChargeAttackCoroutine()
    {
        yield return new WaitForSeconds(chargeDuration);
        if (TempoConductor.Instance.IsOnBeat()) 
            _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerAttack));
        else _enemyContext.StartCoroutine(WaitUntilBeat());
    }
    IEnumerator WaitUntilBeat()
    {
        yield return new WaitForEndOfFrame();
        if (TempoConductor.Instance.IsOnBeat()) 
            _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerAttack));
        else _enemyContext.StartCoroutine(WaitUntilBeat());
    }
}

public class WalkerAttackState : EnemyState
{
    public WalkerAttackState(Enemy enemyContext) : base(enemyContext) { }

    protected override void OnEnter()
    {
        _enemyContext.Animator.SetBool("isWalking", false);
        //_enemyContext.EnemyBaseAttack.UseAttack();
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerDeath));
    }
}
public class WalkerStaggerState : EnemyState
{
    private float staggerDuration;
    private float knockbackForce;

    public WalkerStaggerState(Enemy enemyContext) : base(enemyContext)
    {
        staggerDuration = 0.6f;
        knockbackForce = 0.5f;
    }

    protected override void OnEnter()
    {
        //Debug.Log("Enter Stagger State");
        _enemyContext.Animator.SetBool("isWalking", false);
        _enemyContext.Rigidbody.isKinematic = false;
        Vector3 direction = (PlayerRef.Transform.position - _enemyContext.transform.position).normalized;
        _enemyContext.Rigidbody.AddForce(-(knockbackForce * direction), ForceMode.Impulse);
        _enemyContext.StartCoroutine(StaggerDuration());
    }
    protected override void OnExit()
    {
        _enemyContext.Rigidbody.isKinematic = true;
        _enemyContext.StopAllCoroutines();
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerDeath));
    }

    private IEnumerator StaggerDuration()
    {
        yield return new WaitForSeconds(staggerDuration);
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerChase));
    }
}
public class WalkerDeathState : EnemyState
{
    public WalkerDeathState(Enemy enemyContext) : base(enemyContext) { }

    protected override void OnEnter()
    {
        //Debug.Log("Enter Death State");
        _enemyContext.DropsStrategy.Execute(_enemyContext.transform);
        Object.Destroy(_enemyContext.gameObject);       
    }
}