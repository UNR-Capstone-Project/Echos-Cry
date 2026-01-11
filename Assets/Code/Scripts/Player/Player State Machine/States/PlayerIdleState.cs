using UnityEngine;

public class PlayerIdleState : PlayerActionState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache) 
        : base(playerStateMachine, playerStateCache) {}

    public override void CheckSwitchState()
    {
        if (_playerStateMachine.isMoving)
            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Move));
        else if (_playerStateMachine.isAttacking && TempoManager.CurrentHitQuality != TempoManager.HIT_QUALITY.MISS) 
            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Attack));
    }

    public override void EnterState()
    {
        Debug.Log("Enter Idle State");
    }
}
