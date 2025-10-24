using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MeleeAttack :BaseAttack
{
    private Vector3 swingDirection;
    protected override void InitAttack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        //ISSUE: Use ground layer here to prevent other gameObjects from obstructing the ray.

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;

            swingDirection = -(hitPoint - transform.parent.position).normalized; //Calculates the direction between the mouse mapped to world space and the players position.
            swingDirection.y = 0;

            Quaternion aimingRotation = Quaternion.LookRotation(swingDirection);

            Quaternion decalRotation = Quaternion.Euler(new Vector3(90, aimingRotation.eulerAngles.y, 0));
            transform.rotation = decalRotation;
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
