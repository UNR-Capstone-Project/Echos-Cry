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

public class BatSpawnState : EnemyState
{
    public BatSpawnState(Enemy enemyContext) : base(enemyContext) { }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enter Spawn");
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Idle));
    }
}

public class BatIdleState : EnemyState
{
    private readonly float sqrMagDistance;

    public BatIdleState(Enemy enemyContext) : base(enemyContext)
    {
        sqrMagDistance = Mathf.Pow(10f, 2f);
        TickManager.Instance.GetTimer(0.2f).Tick += Tick;
    }
    ~BatIdleState()
    {
        TickManager.Instance.GetTimer(0.2f).Tick -= Tick;
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enter Idle");
    }
    public override void Tick()
    {
        if (!_isActive) return;
        CheckPlayerDistance();
    }

    private void CheckPlayerDistance()
    {
        if (PlayerRef.Transform == null) return;
        float playerDistance = (_enemyContext.transform.position - PlayerRef.Transform.position).sqrMagnitude;
        if (playerDistance < sqrMagDistance)
        {
            _enemyContext.StateMachine.SwitchState(base._enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Chase));
            return;
        }
    }
}

public class BatChaseState : EnemyState
{
    public BatChaseState(Enemy enemyContext) : base(enemyContext) 
    {
        TickManager.Instance.GetTimer(0.2f).Tick += Tick;
    }
    ~BatChaseState()
    {
        TickManager.Instance.GetTimer(0.2f).Tick -= Tick;
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enter Chase");
        SetEnemyTarget();
    }
    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Exit Chase");
        _enemyContext.StopAllCoroutines();
    }
    public override void Tick()
    {
        if (!_isActive) return;
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
                _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Charge));
        }
    }
    private void SetEnemyTarget()
    {
        if(_enemyContext.NavMeshAgent == null || PlayerRef.Transform == null) return;
        _enemyContext.NavMeshAgent.SetDestination(PlayerRef.Transform.position);
    }
}

public class BatChargeState : EnemyState
{
    private float chargeDuration;

    public BatChargeState(Enemy enemyContext) : base(enemyContext)
    {
        chargeDuration = 1f;
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enter Charge Attack State");
        _enemyContext.StartCoroutine(ChargeAttackCoroutine());
    }
    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Exit Charge Attack State");
        _enemyContext.StopAllCoroutines();
    }

    IEnumerator ChargeAttackCoroutine()
    {
        yield return new WaitForSeconds(chargeDuration);
        if (TempoConductor.Instance.IsOnBeat()) 
            _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Attack));
        else _enemyContext.StartCoroutine(WaitUntilBeat());
    }
    IEnumerator WaitUntilBeat()
    {
        yield return new WaitForEndOfFrame();
        if (TempoConductor.Instance.IsOnBeat()) 
            _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Attack));
        else _enemyContext.StartCoroutine(WaitUntilBeat());
    }
}

public class BatAttackState : EnemyState
{
    private enum AttackStrats
    {
        Melee = 0,
    }
    private bool isAttacking;

    private int MeleeIndex => (int)AttackStrats.Melee;

    public BatAttackState(Enemy enemyContext) : base(enemyContext) { }

    public override void Enter()
    {
        base.Enter();
        isAttacking = true;

        _enemyContext.Rigidbody.isKinematic = false;
        //_enemyContext.Rigidbody.AddForce(dashForce * attackDirection, ForceMode.Impulse);
        _enemyContext.Animator.Play("Attack");
        //_enemyContext.StartCoroutine(AttackDuration());
    }
    public override void Update()
    {
        if (!isAttacking 
            || _enemyContext.AttackStrategies.Length == 0 
            || _enemyContext.AttackStrategies[MeleeIndex] == null) 
            return;

        if (_enemyContext.AttackStrategies[MeleeIndex]
            .Execute(
                10f, 
                _enemyContext.transform.forward, 
                _enemyContext.transform)) 
            isAttacking = false;
    }
    public override void Exit()
    {
        base.Exit();
        _enemyContext.Rigidbody.isKinematic = true;
    }

    //private IEnumerator AttackDuration()
    //{
    //    isAttacking = true;
    //    yield return new WaitForSeconds(attackDuration);
    //    isAttacking = false;
    //    _enemyContext.StartCoroutine(AttackCooldown());
    //}
    //private IEnumerator AttackCooldown()
    //{

    //    yield return new WaitForSeconds(_attackCooldown);
    //    _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Chase));
    //}
}
public class BatStaggerState : EnemyState
{
    private float staggerDuration;
    private float knockbackForce;

    public BatStaggerState(Enemy enemyContext) : base(enemyContext)
    {
        staggerDuration = 1f;
        knockbackForce = 0.5f;
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enter Stagger State");
        _enemyContext.Rigidbody.isKinematic = false;
        Vector3 direction = (PlayerRef.Transform.position - _enemyContext.transform.position).normalized;
        _enemyContext.Rigidbody.AddForce(-(knockbackForce * direction), ForceMode.Impulse);
        _enemyContext.StartCoroutine(StaggerDuration());
    }
    public override void Exit()
    {
        base.Exit();
        _enemyContext.Rigidbody.isKinematic = true;
        _enemyContext.StopAllCoroutines();
    }

    private IEnumerator StaggerDuration()
    {
        yield return new WaitForSeconds(staggerDuration);
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Chase));
    }
}
public class BatDeathState : EnemyState
{
    public BatDeathState(Enemy enemyContext) : base(enemyContext) { }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enter Death State");
        //_enemyContext.Stats.HandleEnemyDeath();
        _enemyContext.DropsStrategy.Execute(_enemyContext.transform);
    }
}