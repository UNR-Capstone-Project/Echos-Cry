using UnityEngine;

public abstract class TargetStrategy : IStrategy
{
    protected Transform target;
    public virtual void Execute() { }
    public virtual Transform Get()
    {
        return target;
    }
}

public class PlayerTargetStrategy : TargetStrategy
{
    public PlayerTargetStrategy(Transform playerTransform)
    {
        target = playerTransform;
    }
}