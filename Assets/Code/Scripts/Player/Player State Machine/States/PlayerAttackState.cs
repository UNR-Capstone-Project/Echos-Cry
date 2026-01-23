using UnityEngine;

public class PlayerAttackState : PlayerActionState
{
    public PlayerAttackState(Player playerContext, PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache) 
        : base(playerContext, playerStateMachine, playerStateCache) { }

    public override void Enter()
    {
        //Initialize whatever attack is happening
        if (_playerStateMachine.UsingPrimaryAction)
        {
            _playerContext.WeaponHolder.PrimaryAction();
        }
        else if (_playerStateMachine.UsingSecondaryAction)
        {
            _playerContext.WeaponHolder.SecondaryAction();
        }
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        //check if current attack is done
        if (Weapon.IsAttackEnded) 
            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Idle));
    }
}
