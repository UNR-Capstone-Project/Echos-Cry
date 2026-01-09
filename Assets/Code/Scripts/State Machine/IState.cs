using UnityEngine;

public interface IState 
{
    public void UpdateState();
    public void EnterState();
    public void ExitState();
    public void CheckSwitchState();
}
