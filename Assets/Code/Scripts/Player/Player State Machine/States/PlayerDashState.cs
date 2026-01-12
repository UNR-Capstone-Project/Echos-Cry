using UnityEngine;

public class PlayerDashState : PlayerActionState
{
    public PlayerDashState(PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache) : base(playerStateMachine, playerStateCache)
    {
    }

    public override void CheckSwitchState()
    {

    }

    public override void EnterState()
    {
        Debug.Log("Enter Dash State");
    }

    public override void ExitState()
    {
     
    }

    public override void UpdateState()
    {
      
    }
}
