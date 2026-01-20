using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerActionState
{
    public PlayerDashState(
        PlayerManager playerContext,
        PlayerStateMachine playerStateMachine, 
        PlayerStateCache playerStateCache) 
        : base(playerContext, playerStateMachine, playerStateCache)
    {}

    public override void Enter()
    {
        _playerContext.PlayerAnimator.SetIsTrailEmit(true);
        _playerContext.PlayerMovement.Dash();
        _playerContext.StartCoroutine(DashDuration());
    }
    public override void Exit()
    {
        _playerContext.PlayerAnimator.SetIsTrailEmit(false);
        _playerStateMachine.IsDashing = false;
    }

    IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(_playerContext.PlayerMovement.PlayerMovementConfig.DashDuration);
        _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Move));
    }
}
