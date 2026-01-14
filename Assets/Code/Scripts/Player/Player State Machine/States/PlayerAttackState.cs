using UnityEngine;

public class PlayerAttackState : PlayerActionState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache) : base(playerStateMachine, playerStateCache) {}

    public override void EnterState()
    {
        if (_playerStateMachine.isLightAttacking)
        {
            //do light attakc
        }
        else if (_playerStateMachine.isHeavyAttacking)
        {
            //do heavy attack
        }
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
    
    }
}
