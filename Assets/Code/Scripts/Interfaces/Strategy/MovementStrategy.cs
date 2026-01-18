using System;
using UnityEngine;

[Serializable]
public abstract class MovementStrategy : ScriptableObject
{
    public abstract void Execute();
}
