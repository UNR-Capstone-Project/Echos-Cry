using UnityEngine;

public class EnemyRangeAttack : EnemyBaseAttack
{
    public override void UseAttack()
    {
        handler = RBProjectileManager.RequestPool(prefab);
        attackDirection = (PlayerRef.PlayerTransform.position - transform.position).normalized;
        attackDirection.y = 0;
        if(handler != null) handler.UseProjectile(transform.position, attackDirection, damage);
        //Transition to whatever state here
    }

    private void Start()
    {

    }

    [SerializeField] private GameObject prefab;
    [SerializeField] private float damage = 5f;
    private Vector3 attackDirection;
    private RBProjectilePool handler;
}
