using System;
using UnityEngine;

[Serializable]
public abstract class MovementStrategy : ScriptableObject
{
    public virtual void Execute(Enemy enemyContext, Vector3 target) { }
}
