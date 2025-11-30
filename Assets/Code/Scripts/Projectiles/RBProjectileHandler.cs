using System;
using UnityEngine;
using UnityEngine.Pool;

//Contains reference to prefab
//Creates ObjectPool of prefab with an initial amount set by user

public class RBProjectileHandler : BaseProjectileHandler
{
    public override void UseProjectile(Vector3 position, Vector3 direction)
    {
        Rigidbody projectileRB = projectilePool.Get();
        projectileRB.linearVelocity = Vector3.zero;
        projectileRB.gameObject.transform.position = position;
        projectileRB.AddForce(direction * projectileSpeed, ForceMode.Impulse);
    }

    private void Awake()
    {
        GameObject holder = GameObject.Find(_projectileSceneHolderName);
        if (holder != null) parentTransform = holder.transform;
        else parentTransform = new GameObject(_projectileSceneHolderName).transform;

        if(prefab.GetComponent<RBPRojectileCollisionHandler>() == null)
        {
            Destroy(gameObject);
            Debug.LogError("Invalid prefab used with " + name + " WTFFFFFFFFFFFFF FIX IT");
        }

        projectilePool = new ObjectPool<Rigidbody>(
            createFunc     : CreateProjectile,
            actionOnGet    : OnGetProjectile,
            actionOnRelease: OnReleaseProjectile,
            actionOnDestroy: OnDestroyProjectile,
            collectionCheck: true,
            defaultCapacity: initialPoolAmount,
            maxSize        : maxPoolAmount
        );
    }
    private Rigidbody CreateProjectile()
    {
        Rigidbody rb = Instantiate(prefab, parentTransform).GetComponent<Rigidbody>();
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
        projectilePool.Release(rb);
    }

    private string _projectileSceneHolderName = "RBProjectileHolder";

    [Header("Prefab & Pool Attributes")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private int initialPoolAmount = 5;
    [SerializeField] private int maxPoolAmount = 25;
    private ObjectPool<Rigidbody> projectilePool;

    [Header("Rigidbody Projectile Attributes")]
    [SerializeField] private float projectileSpeed = 5f;
}
