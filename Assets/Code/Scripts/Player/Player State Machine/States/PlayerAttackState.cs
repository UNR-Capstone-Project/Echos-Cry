using UnityEngine;

public class PlayerAttackState : PlayerActionState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache) : base(playerStateMachine, playerStateCache)
    {
    }

    public override void EnterState()
    {
        if (_playerStateMachine.isLightAttacking)
        {

        }
        else if (_playerStateMachine.isHeavyAttacking)
        {

        }
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
    
    }
}
