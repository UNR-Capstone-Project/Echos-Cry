using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : PlayerActionState
{
    public PlayerIdleState(PlayerManager playerContext, PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache)
        : base(playerContext, playerStateMachine, playerStateCache) { }

    public override void CheckSwitch()
    {
        if (_playerStateMachine.IsMoving)
        {
            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Move));
        }
        else if (_playerStateMachine.IsAttacking && TempoConductor.Instance.IsOnBeat())
        {
            //_playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Attack));
        }
    }
    public override void Enter()
    {
        Debug.Log("Player Idle");
    }
}
