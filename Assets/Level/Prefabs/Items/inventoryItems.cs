using UnityEngine;

public class inventoryItems : MonoBehaviour
{
    public inventoryManager currInventory;
    public inventoryItemData itemData;
    public void pickupItem(){
        currInventory.Add(itemData);
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Player"){
            Debug.Log("collision");
            pickupItem();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
