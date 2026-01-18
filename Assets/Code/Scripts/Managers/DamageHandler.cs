using UnityEngine;
using System.Collections.Generic;

//Main handler for damage to enemies.
//Stream-lined way of handling damage to enemies for large volumes of enemies through queue

//NOTE: This may be unnecessary depending on performance of just using HandleDamageCollision script

public class DamageHandler : MonoBehaviour
{
    //Call this after instantiating EnemyPool
    public void InstantiateEnemyStatsPool(GameObject[] enemyPool)
    {
        int poolLen = enemyPool.Length;
        _enemyStatsPool = new EnemyStats[poolLen];
        for(int i = 0; i < poolLen; i++)
        {
            _enemyStatsPool[i] = enemyPool[i].GetComponent<EnemyStats>();
        }
    }
    //Adds enemy index and amount of damage to the queue
    public void AddToDamageQueue(int index, float damage, Color color)
    {
        _indexQueue.Enqueue(new DamageInfo(index, damage, color));
    }
    //Handles any enemy index placed in queue and does damage to enemy
    public void HandleDamageQueue(int numHandles)
    {
        if (_indexQueue.Count <= 0 || _enemyStatsPool == null) return;

        int handleAmount = numHandles;
        if(_indexQueue.Count < numHandles) handleAmount = _indexQueue.Count; 

        for(int i = 0;i < handleAmount; i++)
        {
            DamageInfo currentInfo = _indexQueue.Dequeue();
            _enemyStatsPool[currentInfo.index].Damage(currentInfo.damage, currentInfo.color);
        } 
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        _indexQueue = new Queue<DamageInfo>();
    }
    private void Update()
    {
        HandleDamageQueue(MAX_HANDLES);
    }

    //Structure to store the index of the enemy being damaged and the amount of damage being done.
    private struct DamageInfo
    {
        public int index;
        public float damage;
        public Color color;
        public DamageInfo(int index, float damage, Color color)
        {
            this.index = index;
            this.damage = damage;
            this.color = color;
        }
    }
    public static DamageHandler Instance { get; private set; }
    //The EnemyStatsPool which will be instantiated with the EnemyPool. This will keep references to each Enemy's EnemyStats script
    private EnemyStats[] _enemyStatsPool;
    private Queue<DamageInfo> _indexQueue;
    private const int MAX_HANDLES = 5;
}
