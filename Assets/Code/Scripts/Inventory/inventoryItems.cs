using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    public InventoryItemData itemData;
    public void PickupItem()
    {
        InventoryManager.Instance.Add(itemData);
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        PickupItem();
    }
}
