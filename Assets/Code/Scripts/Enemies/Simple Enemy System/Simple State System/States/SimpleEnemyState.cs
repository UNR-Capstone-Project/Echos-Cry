
public abstract class SimpleEnemyState
{
    protected SimpleEnemyManager enemyContext;
    public SimpleEnemyState(SimpleEnemyManager enemyContext)
    {
        this.enemyContext = enemyContext;
    }
    public virtual void UpdateState() { }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void CheckSwitchState() { }
}
