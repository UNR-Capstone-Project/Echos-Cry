using static SimpleEnemyStateCache;

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
   
    protected void CheckDeath()
    {
        if (enemyContext.EnemyStats.Health > 0f) return;
        enemyContext.SwitchState(States.BAT_DEATH);
    }
}
