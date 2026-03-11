using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    [SerializeField] protected GameObject particleEffect;
    public InventoryItemData itemData;

    public void PickupItem()
    {
        InventoryManager.Instance.Add(itemData);
        if (particleEffect != null)
        {
            Instantiate(particleEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        PickupItem();
    }
}
