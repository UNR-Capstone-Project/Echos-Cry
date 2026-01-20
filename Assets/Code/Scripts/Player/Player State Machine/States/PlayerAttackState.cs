using UnityEngine;

public class PlayerAttackState : PlayerActionState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache) : base(playerStateMachine, playerStateCache) {}

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
