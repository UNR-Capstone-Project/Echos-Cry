using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

//This is an example inherited SimpleEnemyBehavior script to show what the implementation should look like

public class BasicEnemyBehavior : SimpleEnemyBehavior
{
    private Transform playerTarget;
    [SerializeField] private float interestTimeCooldown = 2f;
    [SerializeField] private float attackWithinDistance = 1f; //Magnitude of vector distance till enemy can attack.
    private bool playerWithinFollowRange = false;
    private bool interestTimerStarted = false;

    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTarget == null)
        {
            Debug.Log("Enemy could not find the Player Game Object.");
        }
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void InitiateEnter()
    {
        Debug.Log("Entering Initiate state!");
        //TODO: Use pooling to instance a enemy attack gameObject.
    }
    public override void InitiateExit()
    {
    
    }
    public override void InitiateSwitchConditions()
    {
    
    }
    public override void InitiateUpdate()
    {
       
    }

    public override void EngagedEnter()
    {
    
    }
    public override void EngagedExit()
    {
     
    }
    public override void EngagedSwitchConditions()
    {
        if (!playerWithinFollowRange)
        {
            SwitchState(_seManager.EnemyStateCache.Unengaged());
        }
        else //Otherwise -> if player is within attacking radius then set state to Initiate attack.
        {
            float playerDistance = Math.Abs((gameObject.transform.position - playerTarget.position).magnitude);
            if (playerDistance < attackWithinDistance)
            {
                SwitchState(_seManager.EnemyStateCache.Initiate());
            }
        }
    }
    public override void EngagedUpdate()
    {
        //Chase the player!
        _enemyNMAgent.SetDestination(playerTarget.position);
    }

    public override void UnengagedEnter()
    {
        Debug.Log("Entering Unengaged state");
    }
    public override void UnengagedExit()
    {
     
    }
    public override void UnengagedSwitchConditions()
    {
        if (playerWithinFollowRange)
        {
            SwitchState(_seManager.EnemyStateCache.Engaged());
        }
    }
    public override void UnengagedUpdate()
    {
        //Enemy idle roaming.
    }

    public override void SpawnEnter()
    {
        Debug.Log("Entering Spawn state");
        SwitchState(_seManager.EnemyStateCache.Unengaged());
    }
    public override void SpawnExit()
    {
        Debug.Log("Exiting Spawn state");
    }
    public override void SpawnSwitchConditions()
    {
        //For now no conditions so just go to roaming.
        SwitchState(_seManager.EnemyStateCache.Unengaged());
    }
    public override void SpawnUpdate()
    {
      
    }

    public override void StaggerEnter()
    {
     
    }
    public override void StaggerExit()
    {
     
    }
    public override void StaggerSwitchConditions()
    {
     
    }
    public override void StaggerUpdate()
    {
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerWithinFollowRange = true;
            interestTimerStarted = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !interestTimerStarted)
        {
            StartCoroutine(loseTargetInterestTimer(interestTimeCooldown));
            interestTimerStarted = true;
        }
    }

    IEnumerator loseTargetInterestTimer(float interestTime)
    {
        yield return new WaitForSeconds(interestTime);
        if (interestTimerStarted)
        {
            playerWithinFollowRange = false;
            interestTimerStarted = false;
        }
    }
}
