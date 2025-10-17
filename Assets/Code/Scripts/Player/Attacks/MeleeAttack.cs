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
            swingDirection = hitPoint - transform.position;
            Quaternion updatedRotation = Quaternion.LookRotation(swingDirection);
            transform.rotation = updatedRotation;
        }

        //ISSUE: Need to change animation to update model position not main gameobject, that way we can rotate towards mouse without animation interference.
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
