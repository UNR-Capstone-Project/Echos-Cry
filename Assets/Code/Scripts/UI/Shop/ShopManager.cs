using UnityEngine;
using TMPro;
using UnityEngine.UI;
/// Original Author: Abby
/// All Contributors Since Creation: Abby
/// Last Modified By:
public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject[] ShopItemArray;
    [SerializeField] private TextMeshProUGUI totalCostText;
    private int currentItemIndex = 0;
    private float totalCost = 0f;

    void Start()
    {
        InputTranslator.OnShopLeftInput += Left;
        InputTranslator.OnShopRightInput += Right;
        InputTranslator.OnShopUpInput += Up;
        InputTranslator.OnShopDownInput += Down;
        InputTranslator.OnPurchaseEvent += Purchase;

        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().ToggleHighlight();
    }
    private void OnDestroy()
    {
        InputTranslator.OnShopLeftInput -= Left;
        InputTranslator.OnShopRightInput -= Right;
        InputTranslator.OnShopUpInput -= Up;
        InputTranslator.OnShopDownInput -= Down;
        InputTranslator.OnPurchaseEvent -= Purchase;
    }

    private void Left()
    {
        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().DecreaseAmount();
    }
    private void Right()
    {
        PlayerStats.UpdateCurrency(100);
        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().IncreaseAmount();
    }
    private void Up()
    {
        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().ToggleHighlight();
        currentItemIndex++;
        if (currentItemIndex >= ShopItemArray.Length) 
        {
            currentItemIndex = 0;
        }
        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().ToggleHighlight();
    }
    private void Down()
    {
        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().ToggleHighlight();
        currentItemIndex--;
        if (currentItemIndex < 0)
        {
            currentItemIndex = ShopItemArray.Length - 1;
        }
        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().ToggleHighlight();
    }

    private void Update()
    {
        float tempCost = 0f;
        foreach (GameObject item in ShopItemArray)
        {
            tempCost += item.GetComponent<ShopItem>().GetCost();
        }
        totalCost = tempCost;
        totalCostText.text = $"Total Cost: ${totalCost.ToString()}";

        if(totalCost <= PlayerStats.CurrencyCount)
        {
            totalCostText.color = Color.white;
        }
        else
        {
            totalCostText.color = Color.red;
        }
    }

    private void Purchase()
    {
        if (totalCost <= 0) return;
        if(PlayerStats.CurrencyCount >= totalCost)
        {
            foreach (GameObject item in ShopItemArray)
            {
                ShopItem shopItemScript = item.GetComponent<ShopItem>();
                for (int i = 0; i < shopItemScript.GetAmount(); i++)
                {
                    Instantiate(shopItemScript.GetPrefab(), GameObject.Find("Player").transform.position, Quaternion.identity);
                }
                shopItemScript.ResetAmount();
            }

            PlayerStats.UpdateCurrency(-(int)totalCost);
        }
    }
}
