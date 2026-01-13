using UnityEngine;

public class PlayerActionInputHandler 
{
    private readonly PlayerStateMachine _playerStateMachine;
    private bool _isLightAttacking, _isHeavyAttacking;
    private Vector2 _locomotion;
   
    public Vector2 Locomotion { get {  return _locomotion; } }

    public PlayerActionInputHandler(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        
        if(_playerStateMachine == null)
        {
            Debug.LogError("Invalid value passed: " + this.GetType().ToString() + " has invalid value passed to constructor");
        }

        _isLightAttacking = false;
        _isHeavyAttacking = false;
    }

    public void BindEvents(InputTranslator translator)
    {
        if (translator == null) return;
        translator.OnMovementEvent += HandleMovement;
        translator.OnHeavyAttackEvent += HandleHeavyAttack;
        translator.OnLightAttackEvent += HandleLightAttack;
        translator.OnDashEvent += HandleDash;
    }
    public void UnbindEvents(InputTranslator translator)
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

        if (_playerStateMachine == null) return;

        if (_locomotion != Vector2.zero) _playerStateMachine.isMoving = true;
        else _playerStateMachine.isMoving = false;
    }
    private void HandleLightAttack(bool buttonPressed)
    {
        if (_playerStateMachine == null) return;
        
        if (buttonPressed)
        {
            _isLightAttacking = true;
            _playerStateMachine.isAttacking = true;
        }
        else
        {
            _isLightAttacking = false;
            if(!_isHeavyAttacking) _playerStateMachine.isAttacking = false;
        }
    }
    private void HandleHeavyAttack(bool buttonPressed)
    {
        if (_playerStateMachine == null) return;

        if (buttonPressed)
        {
            _isHeavyAttacking = true;
            _playerStateMachine.isAttacking = true;
        }
        else
        {
            _isHeavyAttacking = false;
            if(!_isLightAttacking) _playerStateMachine.isAttacking = false;
        }
    }
    private void HandleDash(bool buttonPressed)
    {
        if (_playerStateMachine == null) return;

        if (buttonPressed) _playerStateMachine.isDashing = true;
        else _playerStateMachine.isDashing = false;
    }
}