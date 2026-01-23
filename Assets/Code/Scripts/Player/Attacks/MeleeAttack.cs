using AudioSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using static ComboTree;

public class MeleeAttack : BaseAttack
{
    protected override void InitAttack()
    {
        Quaternion aimingRotation = Quaternion.LookRotation(-PlayerDirection.AimDirection) * Quaternion.AngleAxis(90f, Vector3.right);
        transform.rotation = aimingRotation;
    }
}
