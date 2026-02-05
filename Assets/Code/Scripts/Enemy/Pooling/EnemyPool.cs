using System;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool: MonoBehaviour
{
    private ObjectPool<Enemy> _enemyPool;
    private GameObject _prefab;
    public event Action EnemyReleaseEvent;

    private Enemy CreateEnemy()
    {
        Enemy enemy = GameObject.Instantiate(_prefab, transform).GetComponent<Enemy>();
        enemy.Pool = this;
        return enemy;
    }
    private void OnGetEnemy(Enemy context)
    {
        context.gameObject.SetActive(true);
    }
    private void OnReleaseEnemy(Enemy context)
    {
        context.gameObject.SetActive(false);
    }
    private void OnDestroyEnemy(Enemy context)
    {
        Destroy(context.gameObject);
    }

    public void Init(GameObject prefab, int defaultCap, int maxCap)
    {
        _prefab = prefab;
        if(_prefab == null || !_prefab.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Debug.LogError("ERROR: Invalid prefab passed to EnemyPool. EnemyPool will be destroyed...");
            Destroy(gameObject);
            return;
        }
        _enemyPool = new ObjectPool<Enemy>(
            createFunc: CreateEnemy,
            actionOnGet: OnGetEnemy,
            actionOnRelease: OnReleaseEnemy,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: defaultCap,
            maxSize: maxCap
            );
    }
    public Enemy GetEnemy()
    {
        if(_enemyPool != null) return _enemyPool.Get();
        else return null;
    }
    public void ReleaseEnemy(Enemy enemy)
    {
        _enemyPool?.Release(enemy);
        EnemyReleaseEvent?.Invoke();
    }
}