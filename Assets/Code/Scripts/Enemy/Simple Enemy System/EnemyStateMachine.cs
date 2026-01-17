
using System.Collections;

public class EnemyStateMachine : AbstractStateMachine<EnemyState>
{
    private readonly Enemy _enemyContext;
    public EnemyStateMachine(Enemy enemyContext)
    {
        _enemyContext = enemyContext;
    }
    public void RequestStartCoroutine(IEnumerator coroutine)
    {
        _enemyContext.StartCoroutine(coroutine);
    }
    public void RequestStopCoroutine(IEnumerator coroutine)
    {
        _enemyContext.StopCoroutine(coroutine);
    }
    public void TickState()
    {
        _currentState.Tick();
    }
}
