using System;
using System.Collections;
using UnityEngine;

public class RBPRojectileCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
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
        if (particles != null)
        {
            particles.Stop();
            particles.Clear();
        }
        if (handler != null) handler.ReleaseProjectile(rb);
    }
    public void SetHandler(RBProjectileHandler handler)
    {
        if (this.handler != null) return;
        this.handler = handler;
    }
    public void SetDamage(float damage)
    {
        projectileDamage = damage;
    }

    private void DetermineUserAction()
    {
        switch (user)
        {
            case ProjectileUser.ENEMY:
                damageEnemyAction = (other) => 
                { 
                    if(other.TryGetComponent<PlayerStats>(out PlayerStats playerStats)) 
                        PlayerStats.OnDamageTaken(projectileDamage); 
                };
                break;
            case ProjectileUser.PLAYER:
                damageEnemyAction = (other) =>
                {
                    if (other.TryGetComponent<SimpleEnemyManager>(out SimpleEnemyManager manager)) 
                        manager.EnemyStats.DamageEnemy(projectileDamage);
                };
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        handler = GetComponentInParent<RBProjectileHandler>();
        particles = GetComponentInChildren<ParticleSystem>();
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
    private float projectileDamage = 0;
    private ParticleSystem particles;
    [SerializeField] private float timer = 5;
    [SerializeField] private ProjectileUser user;
}
