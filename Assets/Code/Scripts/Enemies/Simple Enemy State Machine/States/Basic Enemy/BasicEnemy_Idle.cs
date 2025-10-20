using UnityEngine;

public class BasicEnemy_Idle : SimpleEnemyState
{
    public BasicEnemy_Idle(SimpleEnemyStateMachine stateMachineContext) : base(stateMachineContext)
    {
    }
    ~BasicEnemy_Idle()
    {
        SwitchStateEvent -= _stateMachineContext.HandleSwitchState;
    }
    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    protected override void CheckSwitchState()
    {
        throw new System.NotImplementedException();
    }
}
