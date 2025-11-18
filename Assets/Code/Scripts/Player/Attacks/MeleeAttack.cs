using AudioSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using static ComboStateMachine;

public class MeleeAttack : BaseAttack
{
    protected override void InitAttack()
    {
        Vector3 swingDirection;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        int groundMask = LayerMask.GetMask("Ground");

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
        {
            Vector3 hitPoint = hit.point;

            swingDirection = -(hitPoint - transform.parent.position).normalized; //Calculates the direction between the mouse mapped to world space and the players position.
            swingDirection.y = 0;

            Quaternion aimingRotation = Quaternion.LookRotation(swingDirection);

            Quaternion decalRotation = Quaternion.Euler(new Vector3(90, aimingRotation.eulerAngles.y, 0));
            transform.rotation = decalRotation;
        }
    }
}
