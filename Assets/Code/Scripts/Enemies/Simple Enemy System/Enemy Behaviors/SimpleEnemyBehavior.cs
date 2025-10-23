using System;
using UnityEngine;
using UnityEngine.AI;

//Simple Enemy Behavior is the main script that will handle enemy logic for simple enemies

//It is a MonoBehavior script that will be attached to the enemy with the SimpleEnemyManager

//NOTE: when inheriting from SimpleEnemyBehavior, you must override the Awake, Start and OnDestroy functions and use
//base.Awake, base.Start and base.OnDestroy inside of the override functions of the inheritted script

//Each state has 4 functions associated with it
//When transitioning to a new state, an Enter function will be called
//The Update function will be called while the current state is active
//When transitioning to a new state, the Exit function will be called for the current state and then transitioned to the new one

//When inheriting from this script, you can add extra variables and any necessary data for the enemy's behavior
//The base class keeps a reference to the animator and navmesh agent by default

public abstract class SimpleEnemyBehavior : MonoBehaviour
{
    protected SimpleEnemyManager _seManager;
    protected event Action<SimpleEnemyState> SwitchStateEvent;
    protected Animator _enemyAnimator;
    protected NavMeshAgent _enemyNMAgent;

    public virtual void Awake()
    {
        _seManager = GetComponent<SimpleEnemyManager>();
        _enemyAnimator = GetComponent<Animator>();
        _enemyNMAgent = GetComponent<NavMeshAgent>();
    }
    public virtual void Start()
    {
        SwitchStateEvent += _seManager.EnemyStateMachine.HandleSwitchState;
    }
    public virtual void OnDestroy()
    {
        SwitchStateEvent -= _seManager.EnemyStateMachine.HandleSwitchState;
    }

    public abstract void EngagedUpdate();
    public abstract void EngagedEnter();
    public abstract void EngagedExit();
    public abstract void EngagedSwitchConditions();

    public abstract void InitiateUpdate();
    public abstract void InitiateEnter();
    public abstract void InitiateExit();
    public abstract void InitiateSwitchConditions();

    public abstract void SpawnUpdate();
    public abstract void SpawnEnter();
    public abstract void SpawnExit();
    public abstract void SpawnSwitchConditions();

    public abstract void StaggerUpdate();
    public abstract void StaggerEnter();
    public abstract void StaggerExit();
    public abstract void StaggerSwitchConditions();

    public abstract void UnengagedUpdate();
    public abstract void UnengagedEnter();
    public abstract void UnengagedExit();
    public abstract void UnengagedSwitchConditions();
}
