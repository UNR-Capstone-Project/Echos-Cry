using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Strategies/Attack/Ranged")]
public class RangedAttackStrategy : AttackStrategy
{
    [SerializeField] private GameObject _projectilePrefab;
    public override bool Execute(float damage, Vector3 direction, Transform origin)
    {
        RBProjectilePool pool = RBProjectileManager.Instance.RequestPool(_projectilePrefab);
        pool.UseProjectile(origin.position, direction, damage);
        return true;
    }
}
