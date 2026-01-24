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
        _playerStateMachine.IsAttacking = false;
    }

    public override void Update()
    {
        //check if current attack is done
        if (_playerContext.WeaponHolder.IsActionEnded())
        {
            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Idle));
            _playerContext.InvokeAttackEnded();
        }
    }
}
