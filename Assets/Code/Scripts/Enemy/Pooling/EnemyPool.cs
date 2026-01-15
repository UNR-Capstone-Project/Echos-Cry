using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool
{
    private ObjectPool<SimpleEnemyManager> _enemyPool;

    public void Init()
    {
        _enemyPool = new ObjectPool<SimpleEnemyManager>(
            createFunc: CreateEnemy,
            actionOnGet: OnGetEnemy,
            actionOnRelease: OnReleaseEnemy,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: 1,
            maxSize: 1
            );
    }

    private SimpleEnemyManager CreateEnemy()
    {
        return null;
    }
    private void OnGetEnemy(SimpleEnemyManager context)
    {

    }
    private void OnReleaseEnemy(SimpleEnemyManager context)
    {

    }
    private void OnDestroyEnemy(SimpleEnemyManager context)
    {

    }
}
