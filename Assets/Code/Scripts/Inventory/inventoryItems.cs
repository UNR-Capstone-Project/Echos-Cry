using UnityEngine;
/// Original Author: Abby
/// All Contributors Since Creation: Abby
/// Last Modified By:
public class inventoryItems : MonoBehaviour
{
    public inventoryItemData itemData;
    public void pickupItem(){
        InventoryManager.Instance.Add(itemData);
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Player"){
            pickupItem();
        }
    }
}
