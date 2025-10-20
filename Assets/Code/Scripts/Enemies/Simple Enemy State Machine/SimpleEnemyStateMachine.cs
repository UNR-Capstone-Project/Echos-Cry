using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyStateMachine : MonoBehaviour
{
    public SimpleEnemyState CurrentState;
    public SimpleEnemyStateCache StateCache;
    public Animator EnemyAnimator;
    public NavMeshAgent EnemyNavMesh;

    public void HandleSwitchState(SimpleEnemyState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }

    private void Awake()
    {
        EnemyAnimator = GetComponent<Animator>();
        EnemyNavMesh = GetComponent<NavMeshAgent>();

        StateCache = new SimpleEnemyStateCache(this);

        CurrentState = StateCache.Idle();
    }
    private void Start()
    {
        CurrentState.EnterState();
    }
    private void Update()
    {
        CurrentState.UpdateState();
    }
}
