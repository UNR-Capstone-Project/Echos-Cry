using UnityEngine;

public class PlayerIdleState : PlayerActionState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache) 
        : base(playerStateMachine, playerStateCache)
    {
    }

    public override void CheckSwitchState()
    {
        
    }

    public override void EnterState()
    {
        Debug.Log("Enter Idle State");
    }
}
