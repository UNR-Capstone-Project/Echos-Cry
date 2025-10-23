using UnityEngine;

//This is an example inherited SimpleEnemyBehavior script to show what the implementation should look like

public class BasicEnemyBehavior : SimpleEnemyBehavior
{
    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void InitiateEnter()
    {
      
    }
    public override void InitiateExit()
    {
    
    }
    public override void InitiateSwitchConditions()
    {
        
    }
    public override void InitiateUpdate()
    {
       
    }

    public override void EngagedEnter()
    {
    
    }
    public override void EngagedExit()
    {
     
    }
    public override void EngagedSwitchConditions()
    {
     
    }
    public override void EngagedUpdate()
    {
     
    }

    public override void UnengagedEnter()
    {
        Debug.Log("Entering Unengaged state");
    }
    public override void UnengagedExit()
    {
     
    }
    public override void UnengagedSwitchConditions()
    {
     
    }
    public override void UnengagedUpdate()
    {
    
    }

    public override void SpawnEnter()
    {
        Debug.Log("Entering Spawn state");
        SwitchState(_seManager.EnemyStateCache.Unengaged());
    }
    public override void SpawnExit()
    {
        Debug.Log("Exiting Spawn state");
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
