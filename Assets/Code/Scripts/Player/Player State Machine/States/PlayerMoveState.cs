using TMPro;
using UnityEngine;

public class PlayerMoveState : PlayerActionState
{
    private readonly PlayerMovement _playerMovement;
    private readonly PlayerActionInputHandler _inputHandler;
    private readonly PlayerAnimator _animator;

    public PlayerMoveState(PlayerMovement playerMovement,
        PlayerActionInputHandler inputHandler,
        PlayerAnimator playerAnimator,
        PlayerStateMachine playerStateMachine, 
        PlayerStateCache playerStateCache) 
        : base(playerStateMachine, playerStateCache)
    {
        _playerMovement = playerMovement;
        _inputHandler = inputHandler;
        _animator = playerAnimator;
    }

    public override void CheckSwitchState()
    {
        if (!_playerStateMachine.isMoving)
            RequestSwitchState(PlayerStateCache.PlayerState.Idle);
        else if (_playerStateMachine.isAttacking && TempoManager.CurrentHitQuality != TempoManager.HIT_QUALITY.MISS)
            RequestSwitchState(PlayerStateCache.PlayerState.Attack);
        else if (_playerStateMachine.isDashing)
            RequestSwitchState(PlayerStateCache.PlayerState.Dash);
    }

    public override void EnterState()
    {
        Debug.Log("Enter Move State");
        _animator.SetIsMainSpriteRunningAnimation(true);
    }

    public override void ExitState()
    {
        _animator.SetIsMainSpriteRunningAnimation(false);
    }
    public override void UpdateState()
    {
        _animator.UpdateMainSpriteDirection(_inputHandler.Locomotion);
    }

    public override void FixedUpdateState()
    {
        _playerMovement.PlayerMove(_inputHandler.Locomotion);
    }
}
