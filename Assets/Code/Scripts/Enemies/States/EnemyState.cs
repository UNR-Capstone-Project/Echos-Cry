using System;
using UnityEngine;

public abstract class EnemyState
{
    public event Action<EnemyState> SwitchStateEvent;
    public abstract void UpdateState();
    public abstract void EnterState();
    public abstract void ExitState();
    protected abstract void CheckSwitchState();
}
