using System;
using System.Collections;
using UnityEngine;

public class RBPRojectileCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerMeleeAttack")
            && currentUser == ProjectileUser.ENEMY)
        {
            StopAllCoroutines();
            StartCoroutine(WaitForTime());

            //Switch to attacking enemies
            ParryProjectile();
                
            return;
        }

        //Only damage if colliding with valid target
        if ((currentUser == ProjectileUser.ENEMY && other.gameObject.layer != LayerMask.NameToLayer("Player"))
            || (currentUser == ProjectileUser.PLAYER && other.gameObject.layer != LayerMask.NameToLayer("Enemy")))
        {
            return;
        }

        damageEnemyAction(other);
        StopAllCoroutines();
        ResetProjectile();
    }
    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timer);
        ResetProjectile();
    }
    public void ResetProjectile()
    {
        if (handler != null) handler.ReleaseProjectile(rb);
    }
    public void SetHandler(RBProjectilePool handler)
    {
        if (this.handler != null) return;
        this.handler = handler;
    }
    public void SetDamage(float damage)
    {
        projectileDamage = damage;
    }

    private void ParryProjectile()
    {
        currentUser = ProjectileUser.PLAYER;
        DetermineUserAction();

        if (rb == null) return;
        Vector3 projectileDirection = PlayerDirection.AimDirection;
        rb.linearVelocity = projectileDirection * rb.linearVelocity.magnitude;
    }

    private void DetermineUserAction()
    {
        switch (currentUser)
        {
            case ProjectileUser.ENEMY:
                damageEnemyAction = (other) => 
                { 
                    //if (other.TryGetComponent<PlayerStats>(out PlayerStats playerStats)) 
                    //    PlayerStats.Instance.OnDamageTaken(projectileDamage); 
                };
                break;
            case ProjectileUser.PLAYER:
                damageEnemyAction = (other) =>
                {
                    if (other.TryGetComponent<Enemy>(out Enemy manager)) 
                        manager.Stats.Damage(projectileDamage, Color.yellow);
                };
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        handler = GetComponentInParent<RBProjectilePool>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        DetermineUserAction();
    }
    private void OnEnable()
    {
        currentUser = startingUser;
        DetermineUserAction();
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

    private RBProjectilePool handler;
    private Rigidbody rb;
    private Action<Collider> damageEnemyAction;
    private float projectileDamage = 0;
    [SerializeField] private float timer = 5;
    [SerializeField] private ProjectileUser startingUser;
    private ProjectileUser currentUser;
}
