using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttack :BaseAttack
{
    private Vector3 swingDirection;
    protected override void InitAttack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;

            swingDirection = -(hitPoint - transform.position).normalized;
            swingDirection.y = 0;

            Quaternion updatedRotation = Quaternion.LookRotation(swingDirection);
            transform.rotation = updatedRotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(totalAttackDamage);
            other.gameObject.GetComponent<Enemy>().takeKnockBack(swingDirection, knockForce);
        }
    }
}
