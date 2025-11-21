
public abstract class SimpleEnemyState
{
    public virtual void UpdateState(SimpleEnemyManager enemyContext) { }
    public virtual void EnterState(SimpleEnemyManager enemyContext) { }
    public virtual void ExitState(SimpleEnemyManager enemyContext) { }
    public virtual void CheckSwitchState(SimpleEnemyManager enemyContext) { }
}
