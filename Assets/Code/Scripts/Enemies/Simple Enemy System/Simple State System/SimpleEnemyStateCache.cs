
public class SimpleEnemyStateCache 
{
    enum States
    {
        SPAWN = 0, ENGAGED, UNENGAGED, INITIATE, STAGGER
    }
    private const int ARRAY_SIZE = 5;
    private SimpleEnemyState[] _enemyStates;
    public SimpleEnemyStateCache(SimpleEnemyBehavior enemyBehavior)
    {
        _enemyStates = new SimpleEnemyState[ARRAY_SIZE];

        _enemyStates[(int)States.UNENGAGED] = new SimpleEnemyUnengagedState(enemyBehavior);
        _enemyStates[(int)States.ENGAGED]   = new SimpleEnemyEngagedState(enemyBehavior);
        _enemyStates[(int)States.INITIATE]  = new SimpleEnemyInitiateState(enemyBehavior);
        _enemyStates[(int)States.SPAWN]     = new SimpleEnemySpawnState(enemyBehavior);
        _enemyStates[(int)States.STAGGER]   = new SimpleEnemyStaggerState(enemyBehavior);
    }
    public SimpleEnemyState Unengaged()
    {
        return _enemyStates[(int)States.UNENGAGED];
    }
    public SimpleEnemyState Engaged()
    {
        return _enemyStates[(int)States.ENGAGED];
    }
    public SimpleEnemyState Initiate()
    {
        return _enemyStates[(int)States.INITIATE];
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
