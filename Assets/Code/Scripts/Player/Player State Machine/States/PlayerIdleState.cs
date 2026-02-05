using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : PlayerActionState
{
    public PlayerIdleState(Player playerContext, PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache)
        : base(playerContext, playerStateMachine, playerStateCache) { }

    protected override void OnCheckSwitch()
    {
        if (_playerStateMachine.IsMoving)
        {
            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Move));
        }
        else if (_playerStateMachine.IsAttacking)
        {
            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Attack));
        }
    }
    public override void Enter()
    {
        _playerContext.Animator.SpriteAnimator.Play("Idle");
    }
}
