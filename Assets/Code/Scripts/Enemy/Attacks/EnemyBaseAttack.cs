using System;
using UnityEngine;

public abstract class EnemyBaseAttack : MonoBehaviour
{
    protected Enemy _enemyManager;
    public event Action OnEnemyUseAttackEvent;

    protected virtual void Awake()
    {
        _enemyManager = GetComponent<Enemy>();
    }

    protected void RaiseEnemyUseAttack()
    {
        OnEnemyUseAttackEvent?.Invoke();
    }

    public abstract void Use(float damage);
}
