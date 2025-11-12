using AudioSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttack : BaseAttack
{
    private Vector3 swingDirection;
    [SerializeField] soundEffect heavySwingSFX;
    [SerializeField] soundEffect lightSwingSFX;
    protected override void InitAttack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        int groundMask = LayerMask.GetMask("Ground");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Vector3 hitPoint = hit.point;

            swingDirection = -(hitPoint - transform.parent.position).normalized; //Calculates the direction between the mouse mapped to world space and the players position.
            swingDirection.y = 0;

            Quaternion aimingRotation = Quaternion.LookRotation(swingDirection);

            Quaternion decalRotation = Quaternion.Euler(new Vector3(90, aimingRotation.eulerAngles.y, 0));
            transform.rotation = decalRotation;
        }
        
        if (attackType == Attack.AttackType.LIGHT)
        {
            soundEffectManager.Instance.createSound()
                .setSound(lightSwingSFX)
                .setSoundPosition(this.transform.position)
                .ValidateAndPlaySound();
        }
        else if (attackType == Attack.AttackType.HEAVY)
        {
            soundEffectManager.Instance.createSound()
                .setSound(heavySwingSFX)
                .setSoundPosition(this.transform.position)
                .ValidateAndPlaySound();
        }
    }
}
