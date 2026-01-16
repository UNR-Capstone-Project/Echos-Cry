using UnityEngine;

public class EnemyDamageable : IDamageable
{
    EnemyStats _enemyStats;
    public virtual void Init(EnemyStats enemyStats)
    {
        _enemyStats = enemyStats;
    }
    public virtual void Execute(float amount)
    {
        _enemyStats.DamageHealth(amount, Color.red);   
    }
}
