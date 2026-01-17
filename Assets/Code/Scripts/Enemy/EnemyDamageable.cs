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
        if (_enemyStats.HasArmor)
        {
            _enemyStats.DamageArmor(amount, Color.red);
        }
        else
        {
            _enemyStats.DamageHealth(amount, Color.red);   
        }
    }
}
