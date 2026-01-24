using AudioSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Strategies/Attack/Ranged")]
public class RangedAttackStrategy : AttackStrategy
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private soundEffect _attackSound;

    public override bool Execute(float damage, Vector3 direction, Transform origin)
    {
        RBProjectilePool pool = RBProjectileManager.Instance.RequestPool(_projectilePrefab);
        pool.UseProjectile(origin.position, direction, damage);
        if (!SoundEffectManager.Instance.Builder.GetSoundPlayer().IsSoundPlaying())
        {
            SoundEffectManager.Instance.Builder
                .setSound(_attackSound)
                .setSoundPosition(origin.position)
                .ValidateAndPlaySound();
        }
        return true;
    }
}