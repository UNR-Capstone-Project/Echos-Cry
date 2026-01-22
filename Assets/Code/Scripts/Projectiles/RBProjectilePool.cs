using System;
using UnityEngine;
using UnityEngine.Pool;

//Contains reference to prefab
//Creates ObjectPool of prefab with an initial amount set by user

public class RBProjectilePool : MonoBehaviour
{
    public void UseProjectile(Vector3 position, Vector3 direction, float damage)
    {
        Rigidbody projectileRB = projectilePool.Get();
        if (projectileRB.gameObject
            .TryGetComponent<RBPRojectileCollisionHandler>(out RBPRojectileCollisionHandler collisionHandler))
        {
            collisionHandler.SetDamage(damage);
        }

        projectileRB.linearVelocity = Vector3.zero;
        projectileRB.gameObject.transform.position = position;
        projectileRB.AddForce(direction * ProjectileSpeed, ForceMode.Impulse);
    }
    public RBProjectilePool Init(GameObject prefab, int initialPoolAmount, int maxPoolAmount)
    {
        this.prefab = prefab;
        this.initialPoolAmount = initialPoolAmount;
        this.maxPoolAmount = maxPoolAmount;

        projectilePool = new ObjectPool<Rigidbody>(
           createFunc: CreateProjectile,
           actionOnGet: OnGetProjectile,
           actionOnRelease: OnReleaseProjectile,
           actionOnDestroy: OnDestroyProjectile,
           collectionCheck: true,
           defaultCapacity: this.initialPoolAmount,
           maxSize: this.maxPoolAmount
       );

        return this;
    }

    private Rigidbody CreateProjectile()
    {
        Rigidbody rb = Instantiate(prefab, transform).GetComponent<Rigidbody>();
        rb.gameObject.GetComponent<RBPRojectileCollisionHandler>().SetHandler(this);
        return rb;
    }
    private void OnGetProjectile(Rigidbody prefab)
    {
        prefab.gameObject.SetActive(true);
    }
    private void OnReleaseProjectile(Rigidbody prefab)
    {
        prefab.gameObject.SetActive(false);
    }
    private void OnDestroyProjectile(Rigidbody prefab)
    {
        Destroy(prefab.gameObject);
    }

    public void ReleaseProjectile(Rigidbody rb)
    {
        if(!rb.gameObject.activeSelf) return;
        projectilePool.Release(rb);
    }

    //[Header("Prefab & Pool Attributes")]
    private GameObject prefab;
    private int initialPoolAmount = 5;
    private int maxPoolAmount = 25;
    private ObjectPool<Rigidbody> projectilePool;

    [Header("Rigidbody Projectile Attributes")]
    public float ProjectileSpeed = 5f;
}
