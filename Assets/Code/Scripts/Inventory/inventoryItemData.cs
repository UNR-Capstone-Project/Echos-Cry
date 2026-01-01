using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "Echo's Cry/Inventory System/InventoryItemData")]
public class inventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
}
