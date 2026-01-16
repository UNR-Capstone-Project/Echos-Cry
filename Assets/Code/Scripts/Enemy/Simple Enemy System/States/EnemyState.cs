
public abstract class EnemyState : IState
{
    protected EnemyStateMachine _enemyStateMachine;
    protected EnemyStateCache _enemyStateCache;

    protected EnemyManager _enemyContext;
    
    protected EnemyState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache)
    {
        _enemyStateMachine = enemyStateMachine;
        _enemyStateCache = enemyStateCache;
    }

    public void InjectContext(EnemyManager enemyContext)
    {
        _enemyContext = enemyContext;
    }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public virtual void Enter(){ }

    public virtual void Exit() { }

    public virtual void CheckSwitch() { }

    public virtual void UpdateState02ms(EnemyManager enemyContext) { }
}
