
public abstract class EnemyState : IState
{
    protected Enemy _enemyContext;
    protected bool _isActive;
    public bool IsActive { get => _isActive; set => _isActive = value; }

    public EnemyState(Enemy enemyContext)
    {
        _enemyContext = enemyContext;
        _isActive = false;
    }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public virtual void Enter() => _isActive = true;

    public virtual void Exit() => _isActive = false;

    public virtual void CheckSwitch() { }

    //New function added to EnemyState because enemies may have behavior/conditions that do not need to be checked every frame
    public virtual void Tick() { }
}
