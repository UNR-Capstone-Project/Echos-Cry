using UnityEngine;

public class MeleeAttackStrategy : MonoBehaviour, IAttackStrategy
{
    [SerializeField] private readonly Vector3 _boxExtents;
    [SerializeField] private readonly LayerMask _playerMask;

    public bool Execute(float damage, Vector3 direction, float distance, Transform origin)
    {
        if (Physics.BoxCast(
                   center: origin.position,
                   halfExtents: _boxExtents,
                   direction: direction,
                   hitInfo: out RaycastHit hitInfo,
                   orientation: origin.rotation,
                   maxDistance: distance,
                   layerMask: _playerMask))
        {
            hitInfo.collider.gameObject.GetComponent<IDamageable>().Execute(damage);
            return true;
        }
        else return false;
    }
}