using System;
using System.Collections;
using UnityEngine;

public class inventoryDisplay : MonoBehaviour
{
    public slotScript slotOne;
    public slotScript slotTwo;
    public slotScript slotThree;
    public slotScript slotFour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager.current.onInventoryChangedEvent += OnUpdateInventory;
    }
    private void OnUpdateInventory(){
        int i = 1;
        foreach(inventoryItem item in inventoryManager.current.inventory){
            if(i == 1){
                slotOne.Set(item);
                i++;
            }else if(i == 2){
                slotTwo.Set(item);
                i++;
            }else if(i == 3){
                slotThree.Set(item);
                i++;
            }else if(i == 4){
                slotFour.Set(item);
                i++;
            }
        }
    }
    /*public void DrawInventory(){
        foreach(inventoryItem item in inventoryManager.current.inventory){
            AddInventorySlot(item);
        }
    }
    public void AddInventorySlot(inventoryItem item){
        GameObject obj = Instantiate(m_slotPrefab);
        obj.transform.setParent(transform, false);

        UIInventoryItemSlot slot = obj.GetComponent<UIInventoryItemSlot>();
        slot.Set(item);
    }*/
    // Update is called once per frame
    void Update()
    {
        
    }
}
