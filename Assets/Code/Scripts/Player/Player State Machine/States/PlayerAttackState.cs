using UnityEngine;

public class PlayerAttackState : PlayerActionState
{
    public PlayerAttackState(PlayerManager playerContext, PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache) 
        : base(playerContext, playerStateMachine, playerStateCache) { }

    public override void Enter()
    {
        //Initialize whatever attack is happening
        if (_playerStateMachine.IsLightAttacking)
        {
            _playerContext.PlayerAttackHandler.HandleLightInput();
        }
        else if (_playerStateMachine.IsHeavyAttacking)
        {
            _playerContext.PlayerAttackHandler.HandleHeavyInput();
        }
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        //check if current attack is done
        if (BaseWeapon.IsAttackEnded) _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Idle));
    }
}
