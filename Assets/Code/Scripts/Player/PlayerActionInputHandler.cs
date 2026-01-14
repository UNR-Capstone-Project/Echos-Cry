using UnityEngine;

public class PlayerActionInputHandler 
{
    private readonly PlayerStateMachine _playerStateMachine;

    public PlayerActionInputHandler(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        
        if(_playerStateMachine == null)
        {
            Debug.LogError("Invalid value passed: " + this.GetType().ToString() + " has invalid value passed to constructor");
        }
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
        _playerStateMachine.locomotion = locomotion;

        if (_playerStateMachine == null) return;

        if (_playerStateMachine.locomotion != Vector2.zero) _playerStateMachine.isMoving = true;
        else _playerStateMachine.isMoving = false;
    }
    private void HandleLightAttack(bool buttonPressed)
    {
        if (_playerStateMachine == null) return;
        
        if (buttonPressed)
        {
            _playerStateMachine.isLightAttacking = true;
            _playerStateMachine.isAttacking = true;
        }
        else
        {
            _playerStateMachine.isLightAttacking = false;
            if(!_playerStateMachine.isHeavyAttacking) _playerStateMachine.isAttacking = false;
        }
    }
    private void HandleHeavyAttack(bool buttonPressed)
    {
        if (_playerStateMachine == null) return;

        if (buttonPressed)
        {
            _playerStateMachine.isHeavyAttacking = true;
            _playerStateMachine.isAttacking = true;
        }
        else
        {
            _playerStateMachine.isHeavyAttacking = false;
            if(!_playerStateMachine.isLightAttacking) _playerStateMachine.isAttacking = false;
        }
    }
    private void HandleDash(bool buttonPressed)
    {
        if (_playerStateMachine == null) return;

        if (buttonPressed) _playerStateMachine.isDashing = true;
        else _playerStateMachine.isDashing = false;
    }
}