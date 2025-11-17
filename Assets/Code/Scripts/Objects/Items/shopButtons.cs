using UnityEngine;
using TMPro;
public class shopButtons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int cost;
    public inventoryItemData item;
    public inventoryManager currentInventory;
    [SerializeField] private PlayerStats playerStats;
    
    public void buy(){
        if(playerStats.CurrencyCount >= cost){
            currentInventory.Add(item);
            playerStats.subtractCurrency(cost);
        }
    }
    
}
