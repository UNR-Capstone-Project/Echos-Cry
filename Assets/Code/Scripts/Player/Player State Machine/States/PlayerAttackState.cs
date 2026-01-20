using UnityEngine;

public class PlayerAttackState : PlayerActionState
{
    public PlayerAttackState(PlayerManager playerContext, PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache) 
        : base(playerContext, playerStateMachine, playerStateCache) { }

    public override void Enter()
    {
        if (_playerStateMachine.IsLightAttacking)
        {
            //do light attakc
        }
        else if (_playerStateMachine.IsHeavyAttacking)
        {
            //do heavy attack
        }
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
    
    }
}
