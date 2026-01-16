using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool
{
    private ObjectPool<Enemy> _enemyPool;

    public void Init()
    {
        _enemyPool = new ObjectPool<Enemy>(
            createFunc: CreateEnemy,
            actionOnGet: OnGetEnemy,
            actionOnRelease: OnReleaseEnemy,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: 1,
            maxSize: 1
            );
    }

    private Enemy CreateEnemy()
    {
        return null;
    }
    private void OnGetEnemy(Enemy context)
    {

    }
    private void OnReleaseEnemy(Enemy context)
    {

    }
    private void OnDestroyEnemy(Enemy context)
    {

    }
}
