using System;
using System.Collections;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public slotScript[] slotScriptArray = new slotScript[4];
    public slotScript wallet;

    private void OnUpdateInventory(){
        int i = 0;
        foreach(InventoryItem item in InventoryManager.Instance.inventory){
            if(item.data.id == "finger"){
                wallet.Set(item);
            }else if(i == 0){
                slotScriptArray[i].Set(item);
                i++;
            }else if(i == 1){
                slotScriptArray[i].Set(item);
                i++;
            }else if(i == 2){
                slotScriptArray[i].Set(item);
                i++;
            }else if(i == 3){
                slotScriptArray[i].Set(item);
            }
        }
        Debug.Log("slot num "+i);
        slotScriptArray[i].Set(null);
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
        OnUpdateInventory();
    }
}
