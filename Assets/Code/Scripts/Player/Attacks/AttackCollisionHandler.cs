using UnityEngine;

//Handles attack collider.
//When either a melee trigger or projectile trigger collide with the enemy, they will do damage
//Projectiles will be returned to pool/destroyed

public class AttackCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<SimpleEnemyManager>(out SimpleEnemyManager enemyManager))
        {
            enemyManager.EnemyStats.DamageEnemy(AttackDamage);
        }
    }

    public void UpdateAttackDamage(float attackDamage)
    {
        AttackDamage = attackDamage;
    }

    public float AttackDamage { get; private set; }
}