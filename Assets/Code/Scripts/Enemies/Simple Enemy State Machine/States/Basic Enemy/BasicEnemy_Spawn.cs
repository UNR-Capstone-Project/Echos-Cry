using UnityEngine;

public class BasicEnemy_Spawn : SimpleEnemyState
{
    public BasicEnemy_Spawn(SimpleEnemyStateMachine stateMachineContext) : base(stateMachineContext)
    {
    }
    ~BasicEnemy_Spawn() 
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
