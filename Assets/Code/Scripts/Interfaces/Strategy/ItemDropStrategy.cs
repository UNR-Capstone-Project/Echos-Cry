using UnityEngine;

public abstract class ItemDropStrategy : ScriptableObject
{
    [SerializeField] protected ItemDrop[] ItemDrops;

    public abstract void Execute(Transform origin);
}
