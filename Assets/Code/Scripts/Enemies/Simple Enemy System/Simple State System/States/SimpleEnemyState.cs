
public abstract class SimpleEnemyState
{
    protected SimpleEnemyBehavior _enemyBehavior;

    public SimpleEnemyState(SimpleEnemyBehavior enemyBehavior)
    {   
        _enemyBehavior = enemyBehavior;
    }

    public abstract void UpdateState();
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
}
