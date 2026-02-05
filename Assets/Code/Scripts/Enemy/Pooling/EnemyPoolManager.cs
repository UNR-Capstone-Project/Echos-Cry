using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PrefabInfo
{
    public GameObject Prefab;
    public int DefaultCapacity;
    public int MaxCapacity;
}

public class EnemyPoolManager : NonSpawnableSingleton<EnemyPoolManager>
{
    public PrefabInfo[] Prefabs;
    Dictionary<GameObject, EnemyPool> _enemyPools;
    protected override void OnAwake()
    {
        _enemyPools = new Dictionary<GameObject, EnemyPool>();
    }
    private void Start()
    {
        foreach (PrefabInfo prefabNode in Prefabs)
        {
            EnemyPool enemyPool = new GameObject(prefabNode.Prefab.name + " Pool").AddComponent<EnemyPool>();
            enemyPool.Init(prefabNode.Prefab, prefabNode.DefaultCapacity, prefabNode.MaxCapacity);
            _enemyPools.Add(prefabNode.Prefab, enemyPool);
        }
    }

    public EnemyPool GetPool(GameObject prefab)
    {
        if (_enemyPools.ContainsKey(prefab)) return _enemyPools[prefab];
        else return null;
    }
}