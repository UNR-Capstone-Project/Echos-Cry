using UnityEngine;

public abstract class XPCalculationStrategy : ScriptableObject
{
    public abstract float Execute(PlayerStats stats);
}
