using UnityEngine;

public abstract class ItemDropStrategy : ScriptableObject
{
    [SerializeField] protected ItemDrop[] ItemDrops;
    [SerializeField] protected ItemChanceDrop[] ItemChanceDrops;

    public abstract void Execute(Transform origin);
}
