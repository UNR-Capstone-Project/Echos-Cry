using UnityEngine;
using TMPro;
public class shopButtons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int cost;
    public inventoryItemData item;
    public InventoryManager currentInventory;
    //[SerializeField] private PlayerStats playerStats;
    
    public void buy(){
        if(PlayerStats.CurrencyCount >= cost){
            currentInventory.Add(item);
            PlayerStats.UpdateCurrency(-cost);
        }
    }
    
}
