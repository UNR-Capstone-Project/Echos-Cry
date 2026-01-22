using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Scriptable Objects/Strategies/Attack/Melee")]
public class MeleeAttackStrategy : AttackStrategy
{
    [SerializeField] private Vector3 _boxExtents;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _distance;

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
            //Debug.Log("Melee Attack");
            return true;
        }
        else return false;
    }
}