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
    public GameObject shieldHighlight;
    public GameObject healthHighlight;
    private int healthPAmount;
    private int shieldPAmount;
    private int currentRow = 1;
    private int cost;
    //[SerializeField] private PlayerStats playerStats;
    void Start()
    {
        InputTranslator.OnShopLeftInput += Left;
        InputTranslator.OnShopRightInput += Right;
        InputTranslator.OnShopUpInput += Up;
        InputTranslator.OnShopDownInput += Down;
        InputTranslator.OnPurchaseEvent += Purchase;

        healthHighlight.SetActive(true);
        shieldHighlight.SetActive(false);
        cost = 0;
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
        currentRow--;
        //if currentRow does not exist, set to bottom row
        if(currentRow <= 0) currentRow = 2;
        //highlights current row, disables previous row
        if(currentRow == 1){
            healthHighlight.SetActive(true);
            shieldHighlight.SetActive(false);
        }else if(currentRow == 2){
            healthHighlight.SetActive(false);
            shieldHighlight.SetActive(true);
        }
    }
    private void Down(){
        //switches current selected item
        currentRow++;
        if(currentRow >= 3) currentRow = 1;
        //highlights current row, disables previous row
        if(currentRow == 1){
            healthHighlight.SetActive(true);
            shieldHighlight.SetActive(false);
        }else if(currentRow == 2){
            healthHighlight.SetActive(false);
            shieldHighlight.SetActive(true);
        }
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
