using UnityEngine;

public class BasicEnemyBehavior : SimpleEnemyBehavior
{
    public BasicEnemyBehavior(SimpleEnemyManager seManager, SimpleEnemyStateCache stateCache, SimpleEnemyStateMachine stateMachine)
        : base(seManager, stateCache, stateMachine) { }
    ~BasicEnemyBehavior()
    {
        SwitchStateEvent -= _stateMachine.HandleSwitchState;
    }

    public override void AttackEnter()
    {
      
    }
    public override void AttackExit()
    {
    
    }
    public override void AttackSwitchConditions()
    {
        
    }
    public override void AttackUpdate()
    {
       
    }

    public override void ChaseEnter()
    {
    
    }
    public override void ChaseExit()
    {
     
    }
    public override void ChaseSwitchConditions()
    {
     
    }
    public override void ChaseUpdate()
    {
     
    }

    public override void IdleEnter()
    {
    
    }
    public override void IdleExit()
    {
     
    }
    public override void IdleSwitchConditions()
    {
     
    }
    public override void IdleUpdate()
    {
    
    }

    public override void SpawnEnter()
    {
    
    }
    public override void SpawnExit()
    {
     
    }
    public override void SpawnSwitchConditions()
    {
    
    }
    public override void SpawnUpdate()
    {
      
    }

    public override void StaggerEnter()
    {
     
    }
    public override void StaggerExit()
    {
     
    }
    public override void StaggerSwitchConditions()
    {
     
    }
    public override void StaggerUpdate()
    {
     
    }
}
