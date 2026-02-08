using UnityEngine;

//Handles attack collider.
//When either a melee trigger or projectile trigger collide with the enemy, they will do damage
//Projectiles will be returned to pool/destroyed

public class WeaponCollider : MonoBehaviour
{
    [SerializeField] private Weapon _weaponContext;
    [SerializeField] private Collider _collider;
    public float AttackDamage { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamageable>(out IDamageable damagable))
        {
            damagable.Execute(AttackDamage);
            if(_weaponContext != null) _weaponContext.AddColliderToList(other);
        }

        if (other.TryGetComponent<PassiveEffectHandler>(out PassiveEffectHandler passiveEffectHandler))
        {
            if (_weaponContext != null)
            {
                foreach (PassiveEffect effect in _weaponContext.GetPassiveEffects())
                {
                    passiveEffectHandler.UsePassiveEffect(effect);
                }
            }
        }
    }

    public void UpdateAttackDamage(float attackDamage)
    {
        AttackDamage = attackDamage;
    }

}