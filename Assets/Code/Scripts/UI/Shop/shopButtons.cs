using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class shopButtons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int HealthPCost;
    public TMP_Text HealthPCostText;
    public TMP_Text HealthAmountText;
    public int ShieldPCost;
    public TMP_Text ShieldPCostText;
    public TMP_Text ShieldAmountText;
    public GameObject health;
    public GameObject shield;
    //public InventoryManager currentInventory;
    public GameObject shieldHighlight;
    public GameObject healthHighlight;
    public TMP_Text totalText;
    private int healthPAmount;
    private int shieldPAmount;
    private int currentRow = 1;
    private int cost;
    private int tempCost;
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

        //temp to test
        //PlayerStats.UpdateCurrency(5);
    }
    private void OnDestroy()
    {
        InputTranslator.OnShopLeftInput -= Left;
        InputTranslator.OnShopRightInput -= Right;
        InputTranslator.OnShopUpInput -= Up;
        InputTranslator.OnShopDownInput -= Down;
        InputTranslator.OnPurchaseEvent -= Purchase;
    }

    private void Left(){
        //adds one less item to "cart"
        if(currentRow == 1){
            if(healthPAmount != 0) {healthPAmount--; cost -= HealthPCost;}
            tempCost = healthPAmount*HealthPCost;
            HealthPCostText.text = tempCost.ToString();
            HealthAmountText.text = healthPAmount.ToString();
        }else if(currentRow == 2){
            if(shieldPAmount != 0) {shieldPAmount--; cost -= ShieldPCost;}
            tempCost = shieldPAmount*ShieldPCost;
            ShieldPCostText.text = tempCost.ToString();
            ShieldAmountText.text = shieldPAmount.ToString();
        }
        totalText.text = "Total: " + cost.ToString();
        if(cost <= PlayerStats.CurrencyCount){totalText.color = Color.white;}
    }
    private void Right(){
        if(currentRow == 1){
            healthPAmount++;
            cost += HealthPCost;
            tempCost = healthPAmount*HealthPCost;
            HealthPCostText.text = tempCost.ToString();
            HealthAmountText.text = healthPAmount.ToString();
        }else if(currentRow == 2){
            shieldPAmount++;
            cost += ShieldPCost;
            tempCost = shieldPAmount*ShieldPCost;
            ShieldPCostText.text = tempCost.ToString();
            ShieldAmountText.text = shieldPAmount.ToString();
        }
        totalText.text = "Total: " + cost.ToString();
        if(cost > PlayerStats.CurrencyCount){totalText.color = Color.red;}
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
                Instantiate(health, GameObject.Find("Player").transform.position, Quaternion.identity);
            }
            for(int i = 0; i < shieldPAmount; i++){
                Instantiate(shield, GameObject.Find("Player").transform.position, Quaternion.identity);
            }
            PlayerStats.UpdateCurrency(-cost);
            cost = 0;
            shieldPAmount = 0;
            healthPAmount = 0;
            HealthPCostText.text = "0";
            HealthAmountText.text = "0";
            ShieldPCostText.text = "0";
            ShieldAmountText.text = "0";
            totalText.text = "0";
        }else{
            Debug.Log("not enough fingers");
            
            //enable "not enough fingers" message for n seconds
        }
    }
    //destory function

    
}
