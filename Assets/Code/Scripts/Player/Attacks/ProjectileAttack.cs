using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileAttack : BaseAttack
{
    public float projectileSpeed;
    public GameObject destroyParticlePrefab;

    private Vector3 projectileDirection;

    protected override void InitAttack()
    {
        base.InitAttack();

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        int groundMask = LayerMask.GetMask("Ground");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Vector3 hitPoint = hit.point;
            Vector3 targetVec = hitPoint - transform.position;
            projectileDirection = targetVec.normalized;

            if (projectileDirection.y < 0f) { projectileDirection.y = 0f; } //Clamp to angles above 0 degrees horizontally
            projectileDirection.Normalize();

            transform.position += projectileDirection * 0.5f;
        }
    }

    void Update()
    {
        transform.position += projectileDirection * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(totalAttackDamage);
            other.gameObject.GetComponent<Enemy>().takeKnockBack(projectileDirection, knockForce);
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
