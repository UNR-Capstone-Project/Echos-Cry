using UnityEngine;

public abstract class PlayerAbstractState 
{
    public virtual void UpdateState() { }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void CheckSwitchState() { }
}
