
public class SimpleEnemyStateCache 
{
    enum States
    {
        SPAWN = 0, CHASE, IDLE, ATTACK, STAGGER
    }
    private const int ARRAY_SIZE = 5;
    private SimpleEnemyState[] _enemyStates;
    public SimpleEnemyStateCache(SimpleEnemyBehavior enemyBehavior)
    {
        _enemyStates = new SimpleEnemyState[ARRAY_SIZE];

        _enemyStates[(int)States.IDLE]    = new SimpleEnemyIdleState(enemyBehavior);
        _enemyStates[(int)States.CHASE]   = new SimpleEnemyChaseState(enemyBehavior);
        _enemyStates[(int)States.ATTACK]  = new SimpleEnemyAttackState(enemyBehavior);
        _enemyStates[(int)States.SPAWN]   = new SimpleEnemySpawnState(enemyBehavior);
        _enemyStates[(int)States.STAGGER] = new SimpleEnemyStaggerState(enemyBehavior);
    }
    public SimpleEnemyState Idle()
    {
        return _enemyStates[(int)States.IDLE];
    }
    public SimpleEnemyState Chase()
    {
        return _enemyStates[(int)States.CHASE];
    }
    public SimpleEnemyState Attack()
    {
        return _enemyStates[(int)States.ATTACK];
    }
    public SimpleEnemyState Spawn()
    {
        return _enemyStates[(int)States.SPAWN];
    }
    public SimpleEnemyState Stagger()
    {
        return _enemyStates[(int)States.STAGGER];
    }
}
