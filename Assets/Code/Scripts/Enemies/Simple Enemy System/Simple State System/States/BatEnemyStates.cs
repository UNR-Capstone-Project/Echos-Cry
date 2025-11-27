using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static SimpleEnemyStateCache;

//New Implementation of enemy behaviors
//Same as previous system but allows for more customizability for enemy states

//Instead of implementing logic for enemies in hard-coded functions that already exist and prevent customizability for new states,
//This system will just allow you to implement individual states yourself

//Keep enemy states for one enemy in a single script file as shown below

//USE THIS SCRIPT AS A REFERENCE AND GUIDE ON HOW IT WORKS

//NOTE: CheckSwitchState is called automatically in the StateMachine so dont worry about calling it individually

public class BatSpawnState : SimpleEnemyState
{
    public BatSpawnState(SimpleEnemyManager enemyContext) : base(enemyContext) { }

    public override void EnterState()
    {
        //Debug.Log("Enter Spawn State");
        enemyContext.EnemyStateMachine.HandleSwitchState(enemyContext.EnemyStateCache.RequestState(States.BAT_IDLE));
    }
}

public class BatIdleState : SimpleEnemyState
{
    private float sqrMagDistance;
    public BatIdleState(SimpleEnemyManager enemyContext) : base(enemyContext) 
    {
        sqrMagDistance = Mathf.Pow(10f, 2f);
    }

    public override void EnterState()
    {
        //Debug.Log("Enter Idle State");
        TickManager.OnTick05Event += CheckPlayerDistance;
    }
    public override void ExitState()
    {
        TickManager.OnTick05Event -= CheckPlayerDistance;
    }
    public override void CheckSwitchState()
    {
        CheckDeath();   
    }

    private void CheckDeath()
    {
        if (enemyContext.EnemyStats.Health > 0f) return;
        enemyContext.EnemyStateMachine.HandleSwitchState(enemyContext.EnemyStateCache.RequestState(States.BAT_DEATH));
    }
    public  void CheckPlayerDistance()
    {
        float playerDistance = (enemyContext.transform.position - PlayerRef.PlayerTransform.position).sqrMagnitude;
        if (playerDistance < sqrMagDistance)
        {
            enemyContext.EnemyStateMachine.HandleSwitchState(enemyContext.EnemyStateCache.RequestState(States.BAT_CHASE));
            return;
        }
    }
}

public class BatChaseState : SimpleEnemyState
{
    public BatChaseState(SimpleEnemyManager enemyContext) : base(enemyContext) 
    {
    }

    public override void CheckSwitchState()
    {
        CheckDeath();
    }
    public override void EnterState()
    {
        //Debug.Log("Enter Chase State");
        TickManager.OnTick02Event += CheckNavMeshDistance;
        TickManager.OnTick02Event += SetEnemyTarget;
        enemyContext.EnemyNMA.SetDestination(PlayerRef.PlayerTransform.position);
    }
    public override void ExitState()
    {
        TickManager.OnTick02Event -= CheckNavMeshDistance;
        TickManager.OnTick02Event -= SetEnemyTarget;
    }

    private void CheckDeath()
    {
        if (enemyContext.EnemyStats.Health > 0f) return;
        enemyContext.EnemyStateMachine.HandleSwitchState(enemyContext.EnemyStateCache.RequestState(States.BAT_DEATH));
    }
    private void CheckNavMeshDistance()
    {
        NavMeshAgent agent = enemyContext.EnemyNMA;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) 
                enemyContext.EnemyStateMachine.HandleSwitchState(enemyContext.EnemyStateCache.RequestState(States.BAT_CHARGE));
        }
    }
    private void SetEnemyTarget()
    {
        enemyContext.EnemyNMA.SetDestination(PlayerRef.PlayerTransform.position);
    }
}

public class BatChargeAttackState : SimpleEnemyState
{
    private float chargeDuration;

    public BatChargeAttackState(SimpleEnemyManager enemyContext) : base(enemyContext) 
    {
        chargeDuration = 1f;
    }

    public override void CheckSwitchState()
    {
        CheckDeath();
    }
    public override void EnterState()
    {
        //Debug.Log("Enter Charge Attack State");
        enemyContext.StartCoroutine(ChargeAttack());
    }
    public override void ExitState()
    {
        enemyContext.StopAllCoroutines();
    }

    IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(chargeDuration);
        if (TempoManager.CurrentHitQuality != TempoManager.HIT_QUALITY.MISS)
            enemyContext.EnemyStateMachine.HandleSwitchState(enemyContext.EnemyStateCache.RequestState(States.BAT_ATTACK));
        else enemyContext.StartCoroutine(WaitUntilBeat());
    }
    IEnumerator WaitUntilBeat()
    {
        yield return new WaitForEndOfFrame();
        if (TempoManager.CurrentHitQuality != TempoManager.HIT_QUALITY.MISS)
            enemyContext.EnemyStateMachine.HandleSwitchState(enemyContext.EnemyStateCache.RequestState(States.BAT_ATTACK));
        else enemyContext.StartCoroutine(WaitUntilBeat());
    }

    private void CheckDeath()
    {
        if (enemyContext.EnemyStats.Health > 0f) return;
        enemyContext.EnemyStateMachine.HandleSwitchState(enemyContext.EnemyStateCache.RequestState(States.BAT_DEATH));
    }
}

public class BatAttackState : SimpleEnemyState
{
    private Vector3 attackDirection;
    private LayerMask mask;
    private float pushForce;
    private float damage;
    private float attackDuration;
    private float attackCooldown;
    private bool isAttacking;
    public BatAttackState(SimpleEnemyManager enemyContext) : base(enemyContext) 
    {
        mask           = LayerMask.GetMask("Player");
        pushForce      = 6f;
        damage         = 5f;
        attackDuration = 0.25f;
        attackCooldown = 1f;
    }

    public override void CheckSwitchState()
    {
        CheckDeath();
    }

    public override void EnterState()
    {
        //Debug.Log("Enter Attack State");
        isAttacking = true;
        attackDirection = (PlayerRef.PlayerTransform.position - enemyContext.transform.position).normalized;
        attackDirection.y = 0;
        enemyContext.EnemyRigidbody.isKinematic = false;
        enemyContext.EnemyRigidbody.AddForce(pushForce * attackDirection, ForceMode.Impulse);
        enemyContext.StartCoroutine(AttackDuration());
    }

    public override void ExitState()
    {
        enemyContext.EnemyRigidbody.isKinematic = true;
        enemyContext.StopAllCoroutines();
    }

    public override void UpdateState()
    {
        if (!isAttacking) return;
        Attack(enemyContext);
    }
    private IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        enemyContext.StartCoroutine(AttackCooldown());
    }
    private IEnumerator AttackCooldown()
    {
        enemyContext.EnemyRigidbody.isKinematic = true;
        yield return new WaitForSeconds(attackCooldown);
        enemyContext.EnemyStateMachine.HandleSwitchState(enemyContext.EnemyStateCache.RequestState(States.BAT_CHASE));
    }
    private void Attack(SimpleEnemyManager enemyContext)
    {
        if (Physics.BoxCast(enemyContext.transform.position, //Where casting ray
                           new Vector3(0.25f, 0.25f, 0.25f), // size of half of box side lengths
                           attackDirection,                  //Direction of the ray
                           enemyContext.transform.rotation,  //Current enemy rotation
                           1f,                               //Distance ray is cast out
                           mask))                            //Player's layer mask
        {
            PlayerStats.OnDamageTaken(damage);
            isAttacking = false;
        }
    }
    private void CheckDeath()
    {
        if (enemyContext.EnemyStats.Health > 0f) return;
        enemyContext.EnemyStateMachine.HandleSwitchState(enemyContext.EnemyStateCache.RequestState(States.BAT_DEATH));
    }
}
public class BatStaggerState : SimpleEnemyState
{
    private float staggerDuration;
    private float knockbackForce;
    public BatStaggerState(SimpleEnemyManager enemyContext) : base(enemyContext) 
    {
        staggerDuration = 1f;
        knockbackForce = 0.5f;
    }

    public override void CheckSwitchState()
    {
        CheckDeath();
    }
    public override void EnterState()
    {
        //Debug.Log("Enter Stagger State");
        enemyContext.EnemyRigidbody.isKinematic = false;
        Vector3 direction = (PlayerRef.PlayerTransform.position - enemyContext.transform.position).normalized;
        enemyContext.EnemyRigidbody.AddForce(-(knockbackForce * direction), ForceMode.Impulse);
        enemyContext.StartCoroutine(StaggerDuration());
    }
    public override void ExitState()
    {
        enemyContext.EnemyRigidbody.isKinematic = true;
        enemyContext.StopAllCoroutines();
    }

    private void CheckDeath()
    {
        if (enemyContext.EnemyStats.Health > 0f) return;
        enemyContext.EnemyStateMachine.HandleSwitchState(enemyContext.EnemyStateCache.RequestState(States.BAT_DEATH));
    }
    private IEnumerator StaggerDuration()
    {
        yield return new WaitForSeconds(staggerDuration);
        enemyContext.EnemyStateMachine.HandleSwitchState(enemyContext.EnemyStateCache.RequestState(States.BAT_CHASE));
    }
}
public class BatDeathState : SimpleEnemyState
{
    public BatDeathState(SimpleEnemyManager enemyContext) : base(enemyContext) { }

    public override void EnterState()
    {
        //Debug.Log("Enter Death State");
        enemyContext.EnemyStats.HandleEnemyDeath();
    }
}