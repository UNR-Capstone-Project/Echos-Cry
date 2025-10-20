using UnityEngine;

public class BasicEnemy_Chase : SimpleEnemyState
{
    public BasicEnemy_Chase(SimpleEnemyStateMachine stateMachineContext) : base(stateMachineContext)
    {
    }
    ~BasicEnemy_Chase()
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
