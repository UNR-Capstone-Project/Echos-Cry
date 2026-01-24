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
            enemyManager.EnemyStats.DamageEnemy(AttackDamage, AttackColor);

            foreach (PassiveEffect effect in _passiveEffects)
            {
                enemyManager.EnemyStats.UsePassiveEffect(effect);
            }
        }
    }

    public void UpdateAttackDamage(float attackDamage, Color attackColor)
    {
        AttackDamage = attackDamage;
        AttackColor = attackColor;
    }

    public float AttackDamage { get; private set; }
    public Color AttackColor { get; private set; }

    [SerializeField] private PassiveEffect[] _passiveEffects;
}