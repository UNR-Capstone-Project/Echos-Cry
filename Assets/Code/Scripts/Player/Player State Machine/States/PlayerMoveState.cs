using UnityEngine;

public class PlayerMoveState : PlayerActionState
{
    public PlayerMoveState(Player playerContext,
        PlayerStateMachine playerStateMachine, 
        PlayerStateCache playerStateCache)
        : base(playerContext, playerStateMachine, playerStateCache)
    {}

    public override void CheckSwitch()
    {
        if (!_playerStateMachine.IsMoving)
        {
            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Idle));
        }
        else if (_playerStateMachine.IsAttacking && TempoConductor.Instance.IsOnBeat())
        {
            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Attack));
        }
        else if (_playerStateMachine.IsDashing && TempoConductor.Instance.IsOnBeat())
        {
            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Dash));
        }
    }

    public override void Enter()
    {
        _playerContext.Animator.SetIsMainSpriteRunningAnimation(true);
    }

    public override void Exit()
    {
        _playerContext.Animator.SetIsMainSpriteRunningAnimation(false);
    }
    public override void Update()
    {
        _playerContext.Animator.UpdateMainSpriteDirection(_playerStateMachine.Locomotion);
    }

    public override void FixedUpdate()
    {
        _playerContext.Movement.Move(_playerStateMachine.Locomotion);
    }
}
