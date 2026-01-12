using UnityEngine;

public class PlayerActionInputHandler 
{
    private InputTranslator _translator;
    private PlayerStateMachine _playerStateMachine;

    private bool _isLightAttacking, _isHeavyAttacking;

    public PlayerActionInputHandler(InputTranslator translator, PlayerStateMachine playerStateMachine)
    {
        _translator = translator;
        _playerStateMachine = playerStateMachine;

        _isLightAttacking = false;
        _isHeavyAttacking = false;
    }

    public void BindEvents()
    {
        if (_translator == null) return;
        _translator.OnMovementEvent += HandleMovement;
        _translator.OnHeavyAttackEvent += HandleHeavyAttack;
        _translator.OnLightAttackEvent += HandleLightAttack;
        _translator.OnDashEvent += HandleDash;
    }
    public void UnbindEvents()
    {
        if (_translator == null) return;
        _translator.OnMovementEvent -= HandleMovement;
        _translator.OnHeavyAttackEvent -= HandleHeavyAttack;
        _translator.OnLightAttackEvent -= HandleLightAttack;
        _translator.OnDashEvent -= HandleDash;
    }

    private void HandleMovement(Vector2 locomotion)
    {
        if (locomotion != Vector2.zero) _playerStateMachine.isMoving = true;
        else _playerStateMachine.isMoving = false;
    }
    private void HandleLightAttack(bool buttonPressed)
    {
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
        if (buttonPressed) _playerStateMachine.isDashing = true;
        else _playerStateMachine.isDashing = false;
    }
}