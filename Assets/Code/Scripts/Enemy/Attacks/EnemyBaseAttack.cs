using System;
using UnityEngine;

public abstract class EnemyBaseAttack : MonoBehaviour
{
    protected Enemy _enemyManager;
    public static event Action<float> EnemyAttackEvent;

    protected virtual void Awake()
    {
        _enemyManager = GetComponent<Enemy>();
    }
    public abstract void UseAttack();
    protected void InvokeEnemyAttackEvent(float damage)
    {
        EnemyAttackEvent?.Invoke(damage);
    }
}
