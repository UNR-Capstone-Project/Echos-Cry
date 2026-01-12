using UnityEngine;

public class PlayerMoveState : PlayerActionState
{
    private PlayerMovement _playerMovement;
    public PlayerMoveState(PlayerMovement playerMovement, 
        PlayerStateMachine playerStateMachine, 
        PlayerStateCache playerStateCache) 
        : base(playerStateMachine, playerStateCache)
    {
        _playerMovement = playerMovement;
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
    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdateState()
    {
        _playerMovement.PlayerMove();
    }
}
