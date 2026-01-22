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

public class WalkerSpawnState : SimpleEnemyState
{

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        enemyContext.SwitchState(EnemyStates.WALKER_IDLE);
    }
}

public class WalkerIdleState : SimpleEnemyState
{
    private float sqrMagDistance;
    public WalkerIdleState()
    {
        sqrMagDistance = Mathf.Pow(10f, 2f);
    }
    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        enemyContext.EnemyAnimator.SetBool("isWalking", false);
    }

    public override void UpdateState02ms(SimpleEnemyManager enemyContext)
    {
        CheckPlayerDistance(enemyContext);
    }
    public  void CheckPlayerDistance(SimpleEnemyManager enemyContext)
    {
        float playerDistance = (enemyContext.transform.position - PlayerRef.PlayerTransform.position).sqrMagnitude;
        if (playerDistance < sqrMagDistance)
        {
            enemyContext.SwitchState(EnemyStates.WALKER_CHASE);
            return;
        }
    }
}

public class WalkerChaseState : SimpleEnemyState
{
    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        enemyContext.EnemyAnimator.SetBool("isWalking", true);
        SetEnemyTarget(enemyContext);
    }
    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        enemyContext.StopAllCoroutines();
    }
    public override void UpdateState02ms(SimpleEnemyManager enemyContext)
    {
        CheckNavMeshDistance(enemyContext);
        SetEnemyTarget(enemyContext);
    }

    private void CheckNavMeshDistance(SimpleEnemyManager enemyContext)
    {
        NavMeshAgent agent = enemyContext.EnemyNMA;
        if (agent == null) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) enemyContext.SwitchState(EnemyStates.WALKER_CHARGE);
        }
    }
    private void SetEnemyTarget(SimpleEnemyManager enemyContext)
    {
        if(enemyContext.EnemyNMA == null) return;
        enemyContext.EnemyNMA.SetDestination(PlayerRef.PlayerTransform.position);
    }
}

public class WalkerChargeAttackState : SimpleEnemyState
{
    private float chargeDuration;
    public WalkerChargeAttackState()
    {
        chargeDuration = 0.8f;
    }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        //Debug.Log("Enter Charge Attack State");
        enemyContext.EnemyAnimator.SetBool("isWalking", false);
        enemyContext.StartCoroutine(ChargeAttackCoroutine(enemyContext));
    }
    public override void ExitState(SimpleEnemyManager enemyContext)
    {
        //Debug.Log("Exit Charge Attack State");
        enemyContext.StopAllCoroutines();
    }

    IEnumerator ChargeAttackCoroutine(SimpleEnemyManager enemyContext)
    {
        yield return new WaitForSeconds(chargeDuration);
        if (TempoManager.CurrentHitQuality != TempoManager.HIT_QUALITY.MISS) enemyContext.SwitchState(EnemyStates.WALKER_ATTACK);
        else enemyContext.StartCoroutine(WaitUntilBeat(enemyContext));
    }
    IEnumerator WaitUntilBeat(SimpleEnemyManager enemyContext)
    {
        yield return new WaitForEndOfFrame();
        if (TempoManager.CurrentHitQuality != TempoManager.HIT_QUALITY.MISS) enemyContext.SwitchState(EnemyStates.WALKER_ATTACK);
        else enemyContext.StartCoroutine(WaitUntilBeat(enemyContext));
    }
}

public class WalkerAttackState : SimpleEnemyState
{
    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        enemyContext.EnemyAnimator.SetBool("isWalking", false);
        enemyContext.EnemyBaseAttack.UseAttack();
    }
}
public class WalkerStaggerState : SimpleEnemyState
{
    private float staggerDuration;
    private float knockbackForce;
    public WalkerStaggerState()
    {
        staggerDuration = 0.6f;
        knockbackForce = 0.5f;
    }

    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        //Debug.Log("Enter Stagger State");
        enemyContext.EnemyAnimator.SetBool("isWalking", false);
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

    private IEnumerator StaggerDuration(SimpleEnemyManager enemyContext)
    {
        yield return new WaitForSeconds(staggerDuration);
        enemyContext.SwitchState(EnemyStates.WALKER_CHASE);
    }
}
public class WalkerDeathState : SimpleEnemyState
{
    public override void EnterState(SimpleEnemyManager enemyContext)
    {
        //Debug.Log("Enter Death State");
        enemyContext.EnemyStats.HandleEnemyDeath();
    }
}