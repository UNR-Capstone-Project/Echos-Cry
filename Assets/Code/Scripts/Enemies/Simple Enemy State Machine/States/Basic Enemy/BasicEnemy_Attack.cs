using UnityEngine;

public class BasicEnemy_Attack : SimpleEnemyState
{
    public BasicEnemy_Attack(SimpleEnemyStateMachine stateMachineContext) : base(stateMachineContext)
    {
    }
    ~BasicEnemy_Attack()
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
