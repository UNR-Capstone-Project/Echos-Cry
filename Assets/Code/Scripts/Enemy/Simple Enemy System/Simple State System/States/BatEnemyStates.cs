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
        enemyContext.SwitchState(States.BAT_IDLE);
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

    public  void CheckPlayerDistance()
    {
        float playerDistance = (enemyContext.transform.position - PlayerRef.PlayerTransform.position).sqrMagnitude;
        if (playerDistance < sqrMagDistance)
        {
            enemyContext.SwitchState(States.BAT_CHASE);
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

    private void CheckNavMeshDistance()
    {
        NavMeshAgent agent = enemyContext.EnemyNMA;
        if (agent == null) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) enemyContext.SwitchState(States.BAT_CHARGE);
        }
    }
    private void SetEnemyTarget()
    {
        if(enemyContext.EnemyNMA == null) return;
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
        if (TempoManager.CurrentHitQuality != TempoManager.HIT_QUALITY.MISS) enemyContext.SwitchState(States.BAT_ATTACK);
        else enemyContext.StartCoroutine(WaitUntilBeat());
    }
    IEnumerator WaitUntilBeat()
    {
        yield return new WaitForEndOfFrame();
        if (TempoManager.CurrentHitQuality != TempoManager.HIT_QUALITY.MISS) enemyContext.SwitchState(States.BAT_ATTACK);
        else enemyContext.StartCoroutine(WaitUntilBeat());
    }
}

public class BatAttackState : SimpleEnemyState
{

    public BatAttackState(SimpleEnemyManager enemyContext) : base(enemyContext) 
    {
    }

    public override void CheckSwitchState()
    {
        CheckDeath();
    }

    public override void EnterState()
    {
        enemyContext.EnemyBaseAttack.UseAttack();
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

    private IEnumerator StaggerDuration()
    {
        yield return new WaitForSeconds(staggerDuration);
        enemyContext.SwitchState(States.BAT_CHASE);
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