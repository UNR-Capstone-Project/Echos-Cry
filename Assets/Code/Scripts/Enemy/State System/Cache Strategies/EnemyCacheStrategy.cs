using UnityEngine;

public abstract class EnemyCacheStrategy : ScriptableObject
{
    public abstract void Execute(EnemyStateCache stateCache, Enemy enemyContext);
}
