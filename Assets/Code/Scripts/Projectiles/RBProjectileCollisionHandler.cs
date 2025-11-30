using System.Collections;
using UnityEngine;

public class RBPRojectileCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<SimpleEnemyManager>(out SimpleEnemyManager manager))
        {
            manager.EnemyStats.DamageEnemy(10f);
        }
        StopAllCoroutines();
        handler.ReleaseProjectile(rb);
    }
    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timer);
        handler.ReleaseProjectile(rb);
    }
    public void SetHandler(RBProjectileHandler handler)
    {
        if (this.handler != null) return;
        this.handler = handler;
    }

    private void Awake()
    {
        handler = GetComponentInParent<RBProjectileHandler>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        StartCoroutine(WaitForTime());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private RBProjectileHandler handler;
    private Rigidbody rb;
    [SerializeField] private float timer = 5;
}
