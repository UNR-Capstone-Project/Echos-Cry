using UnityEngine;

public class PlayerStateMachine : AbstractStateMachine<PlayerActionState>
{
    private bool _isMoving;
    private bool _isAttacking;
    private bool _usingPrimaryAction, _usingSecondaryAction;
    private bool _isDashing;
    private Vector2 _locomotion;

    public bool IsMoving { get => _isMoving; }
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
    public bool UsingPrimaryAction { get => _usingPrimaryAction; }
    public bool UsingSecondaryAction { get => _usingSecondaryAction; }
    public bool IsDashing { get => _isDashing; set => _isDashing = value; }
    public Vector2 Locomotion { get => _locomotion; }

    public void BindInputs(InputTranslator translator)
    {
        if (translator == null) return;
        translator.OnMovementEvent += HandleMovement;
        translator.OnSecondaryActionEvent += HandleSecondaryAction;
        translator.OnPrimaryActionEvent += HandlePrimaryAction;
        translator.OnDashEvent += HandleDash;
    }
    public void UnbindInputs(InputTranslator translator)
    {
        if (translator == null) return;
        translator.OnMovementEvent -= HandleMovement;
        translator.OnSecondaryActionEvent -= HandleSecondaryAction;
        translator.OnPrimaryActionEvent -= HandlePrimaryAction;
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
    private void HandlePrimaryAction(bool buttonPressed)
    {
        if (buttonPressed)
        {
            _usingPrimaryAction = true;
            _isAttacking = true;
        }
        else
        {
            _usingPrimaryAction = false;
            if (!_usingSecondaryAction) _isAttacking = false;
        }
    }
    private void HandleSecondaryAction(bool buttonPressed)
    {
        if (buttonPressed)
        {
            _usingSecondaryAction = true;
            _isAttacking = true;
        }
        else
        {
            _usingSecondaryAction = false;
            if (!_usingPrimaryAction) _isAttacking = false;
        }
    }
    private void HandleDash(bool buttonPressed)
    {
        if (buttonPressed) _isDashing = true;
        else _isDashing = false;
    }
}
