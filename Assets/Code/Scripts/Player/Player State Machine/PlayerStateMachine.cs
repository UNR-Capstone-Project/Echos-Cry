using UnityEngine;

public class PlayerStateMachine : AbstractStateMachine<PlayerActionState>
{
    public bool isMoving;
    public bool isAttacking;
    public bool isLightAttacking, isHeavyAttacking;
    public bool isDashing;

    public Vector2 locomotion;

    public void BindInputs(InputTranslator translator)
    {
        if (translator == null) return;
        translator.OnMovementEvent += HandleMovement;
        translator.OnHeavyAttackEvent += HandleHeavyAttack;
        translator.OnLightAttackEvent += HandleLightAttack;
        translator.OnDashEvent += HandleDash;
    }
    public void UnbindInputs(InputTranslator translator)
    {
        if (translator == null) return;
        translator.OnMovementEvent -= HandleMovement;
        translator.OnHeavyAttackEvent -= HandleHeavyAttack;
        translator.OnLightAttackEvent -= HandleLightAttack;
        translator.OnDashEvent -= HandleDash;
    }

    private void HandleMovement(Vector2 locomotion)
    {
        this.locomotion = locomotion;

        if (locomotion != Vector2.zero) isMoving = true;
        else isMoving = false;
    }
    private void HandleLightAttack(bool buttonPressed)
    {
        if (buttonPressed)
        {
            isLightAttacking = true;
            isAttacking = true;
        }
        else
        {
            isLightAttacking = false;
            if (!isHeavyAttacking) isAttacking = false;
        }
    }
    private void HandleHeavyAttack(bool buttonPressed)
    {
        if (buttonPressed)
        {
            isHeavyAttacking = true;
            isAttacking = true;
        }
        else
        {
            isHeavyAttacking = false;
            if (!isLightAttacking) isAttacking = false;
        }
    }
    private void HandleDash(bool buttonPressed)
    {
        if (buttonPressed) isDashing = true;
        else isDashing = false;
    }
}
