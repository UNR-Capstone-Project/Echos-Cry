using AudioSystem;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Strategies/Attack/Melee")]
public class MeleeAttackStrategy : AttackStrategy
{
    [SerializeField] private Vector3 _boxExtents;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _distance;
    [SerializeField] private soundEffect _attackSound;

    public override bool Execute(float damage, Vector3 direction, Transform origin)
    {
        if (Physics.BoxCast(
                   center: origin.position,
                   halfExtents: _boxExtents,
                   direction: direction,
                   hitInfo: out RaycastHit hitInfo,
                   orientation: origin.rotation,
                   maxDistance: _distance,
                   layerMask: _playerMask))
        {
            hitInfo.collider.gameObject.GetComponent<IDamageable>().Execute(damage);
            if (!SoundEffectManager.Instance.Builder.GetSoundPlayer().IsSoundPlaying())
            {
                SoundEffectManager.Instance.Builder
                    .SetSound(_attackSound)
                    .SetSoundPosition(origin.position)
                    .ValidateAndPlaySound();
            }
            return true;
        }
        else return false;
    }
}