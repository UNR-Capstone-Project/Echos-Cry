using UnityEngine;

public class PlayerAttackState : PlayerActionState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache) : base(playerStateMachine, playerStateCache)
    {
    }

    public override void CheckSwitchState()
    {
  
    }

    public override void EnterState()
    {
        Debug.Log("Enter Attack State");
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
    
    }
}
