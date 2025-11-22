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
    public BatSpawnState() { }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        Debug.Log("Enter Spawn State");
        enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_IDLE));
    }
}

public class BatIdleState : SimpleEnemyState
{
    private float timer;
    private float maxTime;
    private float sqrMagDistance;
    public BatIdleState() 
    {
        maxTime = 0.1f;
        sqrMagDistance = Mathf.Pow(10f, 2);
    }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        timer = 0;
        Debug.Log("Enter Idle State");
    }
    public override void CheckSwitchState(SimpleEnemyManager enemyContext)
    {
        CheckDeath(enemyContext);   
        CheckPlayerDistance(enemyContext);
    }

    private void CheckDeath(SimpleEnemyManager enemyContext)
    {
        if (enemyContext.EnemyStats.Health > 0f) return;
        enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_DEATH));
    }
    private void CheckPlayerDistance(SimpleEnemyManager enemyContext)
    {
        timer += Time.deltaTime;
        if (timer < maxTime) return;
        timer = 0;
        float playerDistance = (enemyContext.transform.position - PlayerRef.PlayerTransform.position).sqrMagnitude;
        if (playerDistance < sqrMagDistance)
        {
            enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_CHASE));
            return;
        }
    }
}

public class BatChaseState : SimpleEnemyState
{
    private float timer;
    private float maxTime;
    public BatChaseState() 
    {
        maxTime = 0.2f;
    }

    public override void CheckSwitchState(SimpleEnemyManager enemyContext)
    {
        CheckDeath(enemyContext);

        CheckNavMeshDistance(enemyContext); 
    }
    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        Debug.Log("Enter Chase State");
        timer = 0;
        enemyContext.EnemyNMA.SetDestination(PlayerRef.PlayerTransform.position);
    }
    public override void UpdateState(SimpleEnemyManager enemyContext)
    {
        timer += Time.deltaTime;
        if (timer < maxTime) return;

        enemyContext.EnemyNMA.SetDestination(PlayerRef.PlayerTransform.position);
        timer = 0;
    }

    private void CheckDeath(SimpleEnemyManager enemyContext)
    {
        if (enemyContext.EnemyStats.Health > 0f) return;
        enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_DEATH));
    }
    private void CheckNavMeshDistance(SimpleEnemyManager enemyContext)
    {
        NavMeshAgent agent = enemyContext.EnemyNMA;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_CHARGE));
        }
    }
}

public class BatChargeAttackState : SimpleEnemyState
{
    private float chargeDuration;
    private float sqrMagDistance;

    public BatChargeAttackState() 
    {
        chargeDuration = 1f;
        sqrMagDistance = Mathf.Pow(4f, 2);
    }

    public override void CheckSwitchState(SimpleEnemyManager enemyContext)
    {
        CheckDeath(enemyContext);

        CheckPlayerDistance(enemyContext);
    }
    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        Debug.Log("Enter Charge Attack State");
        enemyContext.StartCoroutine(ChargeAttack(enemyContext));
    }
    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        enemyContext.StopAllCoroutines();
    }

    IEnumerator ChargeAttack(SimpleEnemyManager enemyContext)
    {
        yield return new WaitForSeconds(chargeDuration);
        enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_ATTACK));
    }
    private void CheckDeath(SimpleEnemyManager enemyContext)
    {
        if (enemyContext.EnemyStats.Health > 0f) return;
        enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_DEATH));
    }
    private void CheckPlayerDistance(SimpleEnemyManager enemyContext)
    {
        float playerDistance = (enemyContext.transform.position - PlayerRef.PlayerTransform.position).sqrMagnitude;
        if (playerDistance > sqrMagDistance)
        {
            enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_CHASE));
        }
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
    public BatAttackState() 
    {
        mask           = LayerMask.GetMask("Player");
        pushForce      = 5f;
        damage         = 5f;
        attackDuration = 0.15f;
        attackCooldown = 0.35f;
    }

    public override void CheckSwitchState(SimpleEnemyManager enemyContext)
    {
        CheckDeath(enemyContext);
    }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        Debug.Log("Enter Attack State");
        isAttacking = true;
        attackDirection = (PlayerRef.PlayerTransform.position - enemyContext.transform.position).normalized;
        attackDirection.y = 0;
        enemyContext.EnemyRigidbody.isKinematic = false;
        enemyContext.EnemyRigidbody.AddForce(pushForce * attackDirection, ForceMode.Impulse);
        enemyContext.StartCoroutine(AttackDuration(enemyContext));
    }

    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        enemyContext.EnemyRigidbody.isKinematic = true;
        enemyContext.StopAllCoroutines();
    }

    public override void UpdateState(SimpleEnemyManager enemyContext)
    {
        if (!isAttacking) return;
        Attack(enemyContext);
    }
    private IEnumerator AttackDuration(SimpleEnemyManager enemyContext)
    {
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        enemyContext.StartCoroutine(AttackCooldown(enemyContext));
    }
    private IEnumerator AttackCooldown(SimpleEnemyManager enemyContext)
    {
        yield return new WaitForSeconds(attackCooldown);
        enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_CHASE));
    }
    private void Attack(SimpleEnemyManager enemyContext)
    {
        if (Physics.BoxCast(enemyContext.transform.position,
                           new Vector3(0.5f, 0.5f, 0.5f),
                           attackDirection,
                           enemyContext.transform.rotation,
                           2f,
                           mask))
        {
            PlayerStats.OnDamageTaken(damage);
            isAttacking = false;
        }
    }
    private void CheckDeath(SimpleEnemyManager enemyContext)
    {
        if (enemyContext.EnemyStats.Health > 0f) return;
        enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_DEATH));
    }
}
public class BatStaggerState : SimpleEnemyState
{
    private float staggerDuration;
    private float knockbackForce;
    public BatStaggerState() 
    {
        staggerDuration = 1f;
        knockbackForce = 2f;
    }

    public override void CheckSwitchState(SimpleEnemyManager enemyContext)
    {
        CheckDeath(enemyContext);
    }
    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        enemyContext.EnemyRigidbody.isKinematic = false;
        Vector3 direction = (PlayerRef.PlayerTransform.position - enemyContext.transform.position).normalized;
        enemyContext.EnemyRigidbody.AddForce(-(knockbackForce * direction), ForceMode.Impulse);
        enemyContext.StartCoroutine(StaggerDuration(enemyContext));
    }
    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        enemyContext.EnemyRigidbody.isKinematic = true;
        enemyContext.StopAllCoroutines();
    }

    private void CheckDeath(SimpleEnemyManager enemyContext)
    {
        if (enemyContext.EnemyStats.Health > 0f) return;
        enemyContext.EnemyStateMachine.HandleSwitchState(RequestState(States.BAT_DEATH));
    }
    private IEnumerator StaggerDuration(SimpleEnemyManager enemyContext)
    {
        yield return new WaitForSeconds(staggerDuration);
        enemyContext.EnemyStateMachine.HandleSwitchState(RequestState((States)States.BAT_CHASE));
    }
}
public class BatDeathState : SimpleEnemyState
{
    public BatDeathState() { }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        Debug.Log("Enter Death State");
        enemyContext.EnemyStats.HandleEnemyDeath();
    }
}