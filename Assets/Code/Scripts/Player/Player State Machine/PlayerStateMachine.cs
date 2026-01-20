using UnityEngine;

public class PlayerStateMachine : AbstractStateMachine<PlayerActionState>
{
    private bool _isMoving;
    private bool _isAttacking;
    private bool _isLightAttacking, _isHeavyAttacking;
    private bool _isDashing;
    private Vector2 _locomotion;

    public bool IsMoving { get => _isMoving; }
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
    public bool IsLightAttacking { get => _isLightAttacking; }
    public bool IsHeavyAttacking { get => _isHeavyAttacking; }
    public bool IsDashing { get => _isDashing; set => _isDashing = value; }
    public Vector2 Locomotion { get => _locomotion; }

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
        _locomotion = locomotion;

        if (_locomotion != Vector2.zero)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }
    private void HandleLightAttack(bool buttonPressed)
    {
        if (buttonPressed)
        {
            _isLightAttacking = true;
            _isAttacking = true;
        }
        else
        {
            _isLightAttacking = false;
            if (!_isHeavyAttacking) _isAttacking = false;
        }
    }
    private void HandleHeavyAttack(bool buttonPressed)
    {
        if (buttonPressed)
        {
            _isHeavyAttacking = true;
            _isAttacking = true;
        }
        else
        {
            _isHeavyAttacking = false;
            if (!_isLightAttacking) _isAttacking = false;
        }
    }
    private void HandleDash(bool buttonPressed)
    {
        if (buttonPressed) _isDashing = true;
        else _isDashing = false;
    }
}
