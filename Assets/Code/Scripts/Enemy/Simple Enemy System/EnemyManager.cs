using UnityEngine;

public abstract class EnemyManager : MonoBehaviour
{
    private EnemyStateCache   _enemyStateCache;
    private EnemyStateMachine _enemyStateMachine;

    private AttackStrategy   _attackStrategy;
    private TargetStrategy   _targetStrategy;
    private MovementStrategy _movementStrategy;

    protected abstract void Init();

    protected virtual void TickState() { }

    protected virtual void Awake()
    {   
        _enemyStateMachine = new();
        _enemyStateCache = new();
    }
    protected virtual void Start()
    {
        Init();

        TickManager.OnTick02Event += TickState;
    }
    protected virtual void OnDestroy()
    {
        TickManager.OnTick02Event -= TickState;
    }

    protected virtual void Update()
    {
        _enemyStateMachine.Update();
    }
}
