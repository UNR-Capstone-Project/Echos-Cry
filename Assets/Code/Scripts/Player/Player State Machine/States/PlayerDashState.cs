using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerActionState
{
    public PlayerDashState(
        Player playerContext,
        PlayerStateMachine playerStateMachine, 
        PlayerStateCache playerStateCache) 
        : base(playerContext, playerStateMachine, playerStateCache)
    {}

    public override void Enter()
    {
        _playerContext.Animator.SetIsTrailEmit(true);
        _playerContext.Movement.Dash();
        _playerContext.StartCoroutine(DashDuration());
    }
    public override void Exit()
    {
        _playerContext.Animator.SetIsTrailEmit(false);
        _playerStateMachine.IsDashing = false;
    }

    IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(_playerContext.Movement.PlayerMovementConfig.DashDuration);
        _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Move));
    }
}
