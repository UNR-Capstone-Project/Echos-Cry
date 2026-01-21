
using Unity.VisualScripting;

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

    public void Enter()
    {
        _isActive = true;
        OnEnter();
    }
    protected virtual void OnEnter() { } 
    
    public void Exit()
    {
        _isActive = false;
        OnExit();
    }
    protected virtual void OnExit() { }

    public virtual void CheckSwitch() { }

    //New function added to EnemyState because enemies may have behavior/conditions that do not need to be checked every frame
    public virtual void Tick() { }

    protected void CheckDeath(EnemyState deathState)
    {
        if(_enemyContext.Stats.Health <= 0) 
            _enemyContext.StateMachine.SwitchState(deathState);
    }
}
