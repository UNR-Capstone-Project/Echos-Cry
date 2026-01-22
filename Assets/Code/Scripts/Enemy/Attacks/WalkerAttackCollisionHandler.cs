using System.Collections;
using UnityEngine;

public class WalkerAttackCollisionHandler : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float attackDuration = 2f;
    [SerializeField] private float attackWaitBuffer = 0.2f;
    [SerializeField] private ParticleSystem fireRingParticles;
    [SerializeField] private Collider mCollider;
    private bool hit = false;

    private void Start()
    {
        mCollider.enabled = false;
        if (fireRingParticles != null)
        {
            fireRingParticles.Play();
            CameraManager.Instance.ScreenShake(1f, 0.5f);
        }

        Destroy(gameObject, attackDuration);
        StartCoroutine(EnableHitAfterDelay());
    }

    IEnumerator EnableHitAfterDelay()
    {
        yield return new WaitForSeconds(attackWaitBuffer);
        mCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hit)
        {
            PlayerStats.Instance.OnDamageTaken(damageAmount);
            hit = true;
        }
    }
}
