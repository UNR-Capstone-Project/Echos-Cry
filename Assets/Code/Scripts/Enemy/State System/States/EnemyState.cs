
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
    public void Exit()
    {
        _isActive = false;
        OnExit();
    }
    public void Tick()     //New function added to EnemyState because enemies may have behavior/conditions that do not need to be checked every frame
    {
        if (!_isActive) return;
        OnTick();
    }
    public void Enable()
    {
        OnEnable();
    }
    public void Disable()
    {
        OnDisable();
    }

    protected virtual void OnEnter() { }
    protected virtual void OnExit() { }
    public virtual void CheckSwitch() { }
    protected virtual void OnTick() { }
    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }

    protected void CheckDeath(EnemyState deathState)
    {
        if(_enemyContext.Health.CurrentHealth <= 0) 
            _enemyContext.StateMachine.SwitchState(deathState);
    }
}
