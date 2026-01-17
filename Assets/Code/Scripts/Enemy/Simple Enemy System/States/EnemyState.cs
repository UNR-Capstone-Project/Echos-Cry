
public abstract class EnemyState : IState
{
    protected Enemy _enemyContext;

    public EnemyState(Enemy enemyContext)
    {
        _enemyContext = enemyContext;
    }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public virtual void Enter(){ }

    public virtual void Exit() { }

    public virtual void CheckSwitch() { }

    //New function added to EnemyState because enemies may have behavior/conditions that do not need to be checked every frame
    public virtual void Tick() { }
}
