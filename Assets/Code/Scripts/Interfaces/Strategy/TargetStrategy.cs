using System;
using UnityEngine;

[Serializable]
public abstract class TargetStrategy : ScriptableObject
{
    public abstract Vector3 Execute(Transform origin);
}

public class PlayerTargetStrategy : TargetStrategy
{
    public override Vector3 Execute(Transform origin)
    {
        return origin.position;
    }
}