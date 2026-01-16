using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool
{
    private ObjectPool<EnemyManager> _enemyPool;

    public void Init()
    {
        _enemyPool = new ObjectPool<EnemyManager>(
            createFunc: CreateEnemy,
            actionOnGet: OnGetEnemy,
            actionOnRelease: OnReleaseEnemy,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: 1,
            maxSize: 1
            );
    }

    private EnemyManager CreateEnemy()
    {
        return null;
    }
    private void OnGetEnemy(EnemyManager context)
    {

    }
    private void OnReleaseEnemy(EnemyManager context)
    {

    }
    private void OnDestroyEnemy(EnemyManager context)
    {

    }
}
