using UnityEngine;

public class shopButtons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int cost;
    public inventoryItemData item;
    public inventoryManager currentInventory;
    
    public void buy(){
        //add check for fingers
        currentInventory.Add(item);
        //subtract fingers
    }
    
}
