using TMPro;
using UnityEngine;

public class PlayerMoveState : PlayerActionState
{
    private readonly PlayerMovement _playerMovement;
    private readonly PlayerAnimator _animator;

    public PlayerMoveState(PlayerMovement playerMovement,
        PlayerAnimator playerAnimator,
        PlayerStateMachine playerStateMachine, 
        PlayerStateCache playerStateCache) 
        : base(playerStateMachine, playerStateCache)
    {
        _playerMovement = playerMovement;
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
        _animator.SetIsMainSpriteRunningAnimation(true);
    }

    public override void ExitState()
    {
        _animator.SetIsMainSpriteRunningAnimation(false);
    }
    public override void UpdateState()
    {
        _animator.UpdateMainSpriteDirection(_playerStateMachine.locomotion);
    }

    public override void FixedUpdateState()
    {
        _playerMovement.PlayerMove(_playerStateMachine.locomotion);
    }
}
