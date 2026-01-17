
public abstract class EnemyState : IState
{
    protected EnemyStateMachine _enemyStateMachine;
    protected EnemyStateCache _enemyStateCache;

    protected Enemy _enemyContext;
    
    protected EnemyState(EnemyStateMachine enemyStateMachine, EnemyStateCache enemyStateCache)
    {
        _enemyStateMachine = enemyStateMachine;
        _enemyStateCache = enemyStateCache;
    }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public virtual void Enter(){ }

    public virtual void Exit() { }

    public virtual void CheckSwitch() { }

    //New function added to EnemyState because enemies may have behavior/conditions that do not need to be checked every frame
    public virtual void Tick() { }
}
