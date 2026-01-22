using UnityEngine;

public interface IState 
{
    public void Update();
    public void FixedUpdate();
    public void Enter();
    public void Exit();
    public void CheckSwitch();
}
