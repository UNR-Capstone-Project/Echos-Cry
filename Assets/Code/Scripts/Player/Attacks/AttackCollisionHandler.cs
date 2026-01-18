using UnityEngine;

//Handles attack collider.
//When either a melee trigger or projectile trigger collide with the enemy, they will do damage
//Projectiles will be returned to pool/destroyed

public class AttackCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Enemy>(out Enemy enemyManager))
        {
            enemyManager.Stats.Damage(AttackDamage, AttackColor);
        }
    }

    public void UpdateAttackDamage(float attackDamage, Color attackColor)
    {
        AttackDamage = attackDamage;
        AttackColor = attackColor;
    }

    public float AttackDamage { get; private set; }
    public Color AttackColor { get; private set; }
}