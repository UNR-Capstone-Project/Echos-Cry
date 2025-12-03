using UnityEngine;

public class EnemyRangeAttack : EnemyBaseAttack
{
    public override void UseAttack()
    {
        attackDirection = (PlayerRef.PlayerTransform.position - transform.position).normalized;
        attackDirection.y = 0;
        if(handler != null) handler.UseProjectile(transform.position, attackDirection, damage);
        //Transition to whatever state here
    }

    private void Start()
    {
        handler = RBProjectileManager.RequestHandler(prefab);
    }

    [SerializeField] private GameObject prefab;
    [SerializeField] private float damage = 5f;
    private Vector3 attackDirection;
    private RBProjectileHandler handler;
}
