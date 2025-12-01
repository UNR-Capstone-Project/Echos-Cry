using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class shopButtons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int HealthPCost;
    public Text HealthPCostText;
    public int ShieldPCost;
    public Text ShieldPCostText;
    public inventoryItemData health;
    public inventoryItemData shield;
    public InventoryManager currentInventory;
    private int healthPAmount;
    private int shieldPAmount;
    //[SerializeField] private PlayerStats playerStats;
    void Start()
    {
        InputTranslator.OnShopLeftInput += Left;
        InputTranslator.OnShopRightInput += Right;
        InputTranslator.OnShopUpInput += Up;
        InputTranslator.OnShopDownInput += Down;
        InputTranslator.OnPurchaseEvent += Purchase;
    }
    private void Left(){
        //adds one less item to "cart"
        //updates price for all items of this type
    }
    private void Right(){
        //adds one more item to "cart"
        //updates price
    }
    private void Up(){
        //switches current selected item
        //highlights row
    }
    private void Down(){
        //switches current selected item
        //highlights row
    }
    private void Purchase(){
        if(PlayerStats.CurrencyCount >= cost){
            for(int i = 0; i < healthPAmount; i++){
                currentInventory.Add(health); 
            }
            for(int i = 0; i < shieldPAmount; i++){
                currentInventory.Add(shield);
            }
            PlayerStats.UpdateCurrency(-cost);
        }else{
            //enable "not enough fingers" message for n seconds
        }
    }
    //destory function

    
}
