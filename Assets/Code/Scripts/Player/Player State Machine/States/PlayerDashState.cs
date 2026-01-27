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
        _playerContext.Animator.SpriteAnimator.Play("Dash");
        _playerContext.SFX.Execute(_playerContext.SFXConfig.DashSFX, _playerContext.transform, 0);
        _playerContext.Movement.Dash();
        _playerStateMachine.CanDash = false;
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
        if (_playerContext.Movement.PlayerMovementConfig.HasDashCooldown) _playerContext.StartCoroutine(DashCooldown());
        else _playerStateMachine.CanDash = true;
    }
    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(_playerContext.Movement.PlayerMovementConfig.DashCooldown);
        _playerStateMachine.CanDash = true;
    }
}
