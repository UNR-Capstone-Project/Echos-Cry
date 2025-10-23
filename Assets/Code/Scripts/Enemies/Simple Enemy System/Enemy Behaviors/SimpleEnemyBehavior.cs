using System;
using UnityEngine;
using UnityEngine.AI;

//DO NOT ADJUST BASE CLASS UNLESS STRICTLY NECESSARY

///////////////////// SUMMARY //////////////////
//Simple Enemy Behavior is the main script that will handle enemy logic for simple enemies

//It is a MonoBehavior script that will be attached to the enemy with the SimpleEnemyManager

//NOTE: when inheriting from SimpleEnemyBehavior, you must override the Awake, Start and OnDestroy functions and use
//base.Awake, base.Start and base.OnDestroy inside of the override functions of the inheritted script

///////////////////// STATES //////////////////
//There are 5 total states
//SPAWN: initial state on startup of enemy. This will contain any animations or logic associaited with the enemy spawning in the scene and will be the first state entered
//UNENGAGED: This is the enemy logic whenever the enemy is not engaged with the player. Whether it's roaming or just standing there
//ENGAGED: The logic when the enemy is engaged to the player, whether it is chasing the player or moving in positions around the player
//INITIATE: This is the state that occurs when the enemy attacks/interacts with the player. Should only reached this state from ENGAGED state
//STAGGER: This state occurs when the player hits the enemy. Some logic for a stun to the enemy before entering ENGAGED state

///////////////////// STATE FUNCTIONS //////////////////
//Each state has 4 functions associated with it
//When transitioning to a new state, an Enter function will be called
//The Update function will be called while the current state is active
//When transitioning to a new state, the Exit function will be called for the current state and then transitioned to the new one

///////////////////// INHERITING //////////////////
//When inheriting from this script, you can add extra variables and any necessary data for the enemy's behavior to the inherited script
//The base class keeps a reference to the animator and navmesh agent by default

//These will implement these components on the gameObject if they don't already exist
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class SimpleEnemyBehavior : MonoBehaviour
{
    protected     SimpleEnemyManager       _seManager;
    private event Action<SimpleEnemyState> SwitchStateEvent;
    protected     Animator                 _enemyAnimator;
    protected     NavMeshAgent             _enemyNMAgent;

    //Needs to be used to call SwitchStateEvent from an inheritted class since it can't be called from outside SimpleEnemyBehavior
    protected void SwitchState(SimpleEnemyState enemyState)
    {
        SwitchStateEvent?.Invoke(enemyState);
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

    public virtual void Awake()
    {
        _seManager     = GetComponent<SimpleEnemyManager>();
        _enemyAnimator = GetComponent<Animator>();
        _enemyNMAgent  = GetComponent<NavMeshAgent>();
    }
    public virtual void Start()
    {
        SwitchStateEvent += _seManager.EnemyStateMachine.HandleSwitchState;
    }
    public virtual void OnDestroy()
    {
        SwitchStateEvent -= _seManager.EnemyStateMachine.HandleSwitchState;
    }
}
