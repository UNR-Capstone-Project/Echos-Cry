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

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            swingDirection = -(hit.point - transform.parent.position).normalized; //Calculates the direction between the mouse mapped to world space and the players position.
            swingDirection.y = 0;
            Quaternion aimingRotation = Quaternion.LookRotation(swingDirection) * Quaternion.AngleAxis(90f, Vector3.right);
            transform.rotation = aimingRotation;
        }
    }
}
