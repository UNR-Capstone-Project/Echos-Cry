using static SimpleEnemyStateCache;

public abstract class SimpleEnemyState
{
    public virtual void UpdateState(SimpleEnemyManager enemyContext) { }
    public virtual void EnterState(SimpleEnemyManager enemyContext) { }
    public virtual void ExitState(SimpleEnemyManager enemyContext) { }
    public virtual void CheckSwitchState(SimpleEnemyManager enemyContext) { }
   
    protected void CheckDeath(SimpleEnemyManager enemyContext)
    {
        if (enemyContext.EnemyStats.Health > 0f) return;
        enemyContext.SwitchState(EnemyStates.BAT_DEATH);
    }
}
