using NUnit.Framework.Constraints;
using System.Collections;
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
    WalkerConfig _config;

    public WalkerIdleState(WalkerConfig config, Enemy enemyContext) : base(enemyContext)
    {
        _config = config;
        TickManager.Instance.GetTimer(0.2f).Tick += Tick;
    }
    ~WalkerIdleState() 
    {
        TickManager.Instance.GetTimer(0.2f).Tick -= Tick;
    }

    protected override void OnEnter()
    {
        _enemyContext.Animator.SetBool("isWalking", false);
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerDeath));
    }

    protected override void OnTick()
    {
        CheckPlayerDistance();
    }
    public  void CheckPlayerDistance()
    {
        float playerDistance = Vector3.Distance(PlayerRef.Transform.position, _enemyContext.transform.position);
        if (playerDistance < _config.DistanceCheck)
        {
            _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerChase));
            return;
        }
    }
}

public class WalkerChaseState : EnemyState
{
    public WalkerChaseState(Enemy enemyContext) : base(enemyContext) 
    {
        TickManager.Instance.GetTimer(0.2f).Tick += Tick;
    }
    ~WalkerChaseState()
    {
        TickManager.Instance.GetTimer(0.2f).Tick -= Tick;
    }

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
    protected override void OnTick()
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
    WalkerConfig _config;

    public WalkerChargeAttackState(WalkerConfig config, Enemy enemyContext) : base(enemyContext)
    {
        _config = config;
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
        yield return new WaitForSeconds(_config.AttackChargeTime);
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
    WalkerConfig _config;
    bool isAttacking;
    Vector3 attackDirection;
    public WalkerAttackState(WalkerConfig config, Enemy enemyContext) : base(enemyContext) 
    { 
        _config = config;
    }

    private IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(_config.AttackDuration);
        isAttacking = false;
        _enemyContext.StartCoroutine(AttackCooldown());
    }
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_config.AttackCooldown);
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerChase));
    }

    protected override void OnEnter()
    {
        isAttacking = true;
        _enemyContext.Animator.SetBool("isWalking", false);
        _enemyContext.Rigidbody.isKinematic = false;
        attackDirection = (PlayerRef.Transform.position - _enemyContext.transform.position).normalized;
        _enemyContext.Rigidbody.AddForce(_config.AttackDashForce * attackDirection, ForceMode.Impulse);
        _enemyContext.StartCoroutine(AttackDuration());
    }
    protected override void OnExit()
    {
        _enemyContext.Rigidbody.isKinematic = true;
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerDeath));
    }
    public override void Update()
    {
        if (!isAttacking
            || _enemyContext.AttackStrategies.Length == 0
            || _enemyContext.AttackStrategies[0] == null)
            return;

        if (_enemyContext.AttackStrategies[0]
            .Execute(
                10f,
                attackDirection,
                _enemyContext.transform))
            isAttacking = false;
    }
}
public class WalkerStaggerState : EnemyState
{
    WalkerConfig _config;

    public WalkerStaggerState(WalkerConfig config, Enemy enemyContext) : base(enemyContext)
    {
        _config = config;
    }

    protected override void OnEnter()
    {
        //Debug.Log("Enter Stagger State");
        _enemyContext.Animator.SetBool("isWalking", false);
        _enemyContext.Rigidbody.isKinematic = false;
        Vector3 direction = (PlayerRef.Transform.position - _enemyContext.transform.position).normalized;
        _enemyContext.Rigidbody.AddForce(-(_config.KnockbackForce * direction), ForceMode.Impulse);
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
        yield return new WaitForSeconds(_config.StaggerDuration);
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