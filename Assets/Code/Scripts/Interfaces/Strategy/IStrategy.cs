using UnityEngine;

public interface IStrategy
{
    public void Execute();
}

public abstract class MovementStrategy : IStrategy
{
    public abstract void Execute();
}

public abstract class TargetStrategy : IStrategy
{
    protected Transform target;
    public abstract void Execute();
    public virtual Transform Get()
    {
        return target;
    }
}