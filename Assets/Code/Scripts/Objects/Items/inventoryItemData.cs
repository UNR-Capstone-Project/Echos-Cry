using UnityEngine;

[CreateAssetMenu(fileName = "inventoryItemData", menuName = "Scriptable Objects/inventoryItemData")]
public class inventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
}
