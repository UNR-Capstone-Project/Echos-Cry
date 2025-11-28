using AudioSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using static ComboStateMachine;

public class MeleeAttack : BaseAttack
{
    protected override void InitAttack()
    {
        Quaternion aimingRotation = Quaternion.LookRotation(-PlayerDirection.AimDirection) * Quaternion.AngleAxis(90f, Vector3.right);
        transform.rotation = aimingRotation;
    }
}
