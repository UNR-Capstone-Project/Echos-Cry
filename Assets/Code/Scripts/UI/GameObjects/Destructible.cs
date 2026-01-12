using UnityEngine;

public class Destructible : EnemyDrops
{
    [SerializeField] private GameObject destroyedVersion;

    protected void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<AttackCollisionHandler>(out AttackCollisionHandler handler))
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            HandleEnemyDrops();
            Destroy(gameObject);
        }
    }

    public override void Start()
    {
        //Override enemy behaviors
    }
    public override void OnDestroy()
    {
        //Override enemy behaviors
    }
}
