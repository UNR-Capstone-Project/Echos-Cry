using UnityEngine;

[System.Serializable]
public struct ItemDrop
{
    public GameObject prefab;
    public int minDropAmount;
    public int maxDropAmount;
}

[System.Serializable]
public struct ItemChanceDrop
{
    public GameObject prefab;
    public int dropAmount;
    public float dropChancePercent;
}
