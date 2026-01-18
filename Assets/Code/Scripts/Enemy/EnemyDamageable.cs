using UnityEngine;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] EnemyStats _enemyStats;
    public virtual void Init(EnemyStats enemyStats)
    {
        _enemyStats = enemyStats;
    }
    public virtual void Execute(float amount)
    {
        _enemyStats.Damage(amount, Color.red);
    }
}
