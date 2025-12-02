using System;
using System.Collections;
using UnityEngine;

public class RBPRojectileCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        damageEnemyAction(other);
        StopAllCoroutines();
        if(handler != null) handler.ReleaseProjectile(rb);
    }
    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timer);
        if (handler != null) handler.ReleaseProjectile(rb);
    }
    public void SetHandler(RBProjectileHandler handler)
    {
        if (this.handler != null) return;
        this.handler = handler;
    }

    private void DetermineUserAction()
    {
        switch (user)
        {
            case ProjectileUser.ENEMY:
                damageEnemyAction = (other) => { PlayerStats.OnDamageTaken(10f); };
                break;
            case ProjectileUser.PLAYER:
                damageEnemyAction = (other) =>
                {
                    if (other.TryGetComponent<SimpleEnemyManager>(out SimpleEnemyManager manager))
                    {
                        manager.EnemyStats.DamageEnemy(10f);
                    }
                };
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        handler = GetComponentInParent<RBProjectileHandler>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        DetermineUserAction();
    }
    private void OnEnable()
    {
        StartCoroutine(WaitForTime());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public enum ProjectileUser
    {
        UNASSIGNED = 0, PLAYER, ENEMY
    }

    private RBProjectileHandler handler;
    private Rigidbody rb;
    private Action<Collider> damageEnemyAction;
    [SerializeField] private float timer = 5;
    [SerializeField] private ProjectileUser user;
}
