using System;
using UnityEngine;

public abstract class EnemyBaseAttack : MonoBehaviour
{
    protected Enemy _enemyManager;
    public static event Action<float> AttackEvent;

    protected virtual void Awake()
    {
        _enemyManager = GetComponent<Enemy>();
    }
    public virtual void Use(float damage)
    {
        InvokeAttackEvent(damage);
    }
    protected void InvokeAttackEvent(float damage)
    {
        AttackEvent?.Invoke(damage);
    }
}
