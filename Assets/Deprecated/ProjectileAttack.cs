using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileAttack : BaseAttack
{
    public float projectileSpeed;
    public GameObject destroyParticlePrefab;

    private Vector3 projectileDirection;
    private Rigidbody projectileRB;

    private void Awake()
    {
        projectileRB = GetComponent<Rigidbody>();
    }

    protected override void InitAttack()
    {
        base.InitAttack();

        //projectileDirection = PlayerOrientation.Direction;
        //projectileRB.AddForce(projectileDirection * projectileSpeed, ForceMode.Impulse);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //other.gameObject.GetComponent<LegacyEnemy>().takeDamage(totalAttackDamage);
            //other.gameObject.GetComponent<LegacyEnemy>().takeKnockBack(projectileDirection, knockForce);
            StartDestroy(0);
        }
        else if (!other.gameObject.CompareTag("Player"))
        {
            StartDestroy(0);
        }
    }

    void OnDestroy()
    {
        GameObject particleInstance = Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
    }
}