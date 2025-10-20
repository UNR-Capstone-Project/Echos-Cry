
public class SimpleEnemyStateCache 
{
    enum States
    {
        SPAWN = 0, CHASE, IDLE, ATTACK, STAGGER
    }
    private const int ARRAY_SIZE = 5;
    private SimpleEnemyState[] _enemyStates;
    public SimpleEnemyStateCache(SimpleEnemyStateMachine stateMachineContext)
    {
        _enemyStates = new SimpleEnemyState[ARRAY_SIZE];
        _enemyStates[(int)States.IDLE] = new BasicEnemy_Idle(stateMachineContext);
        _enemyStates[(int)States.CHASE] = new BasicEnemy_Chase(stateMachineContext);
        _enemyStates[(int)States.ATTACK] = new BasicEnemy_Attack(stateMachineContext);
        _enemyStates[(int)States.SPAWN] = new BasicEnemy_Spawn(stateMachineContext);
        _enemyStates[(int)States.STAGGER] = new BasicEnemy_Stagger(stateMachineContext);
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
