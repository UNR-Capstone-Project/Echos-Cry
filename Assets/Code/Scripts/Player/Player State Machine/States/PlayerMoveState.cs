
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

    public override void CheckSwitch()
    {
        if (!_playerStateMachine.isMoving)
            RequestSwitchState(PlayerStateCache.PlayerState.Idle);
        else if (_playerStateMachine.isAttacking && TempoManager.CurrentHitQuality != TempoManager.HIT_QUALITY.MISS)
            RequestSwitchState(PlayerStateCache.PlayerState.Attack);
        else if (_playerStateMachine.isDashing)
            RequestSwitchState(PlayerStateCache.PlayerState.Dash);
    }

    public override void Enter()
    {
        _animator.SetIsMainSpriteRunningAnimation(true);
    }

    public override void Exit()
    {
        _animator.SetIsMainSpriteRunningAnimation(false);
    }
    public override void Update()
    {
        _animator.UpdateMainSpriteDirection(_playerStateMachine.locomotion);
    }

    public override void FixedUpdate()
    {
        _playerMovement.Move(_playerStateMachine.locomotion);
    }
}
