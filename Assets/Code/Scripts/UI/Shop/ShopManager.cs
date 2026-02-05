using UnityEngine;
using TMPro;
using UnityEngine.UI;
/// Original Author: Abby
/// All Contributors Since Creation: Abby
/// Last Modified By:
public class ShopManager : MonoBehaviour
{
    [SerializeField] private InputTranslator _translator;
    [SerializeField] private GameObject[] ShopItemArray;
    [SerializeField] private TextMeshProUGUI totalCostText;
    [SerializeField] private TextMeshProUGUI currentGoldText;
    private int currentItemIndex = 0;
    private float totalCost = 0f;

    void Start()
    {
        _translator.OnShopLeftInput += Left;
        _translator.OnShopRightInput += Right;
        _translator.OnShopUpInput += Up;
        _translator.OnShopDownInput += Down;
        _translator.OnPurchaseEvent += Purchase;

        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().ToggleHighlight(true);
    }
    private void OnDestroy()
    {
        _translator.OnShopLeftInput -= Left;
        _translator.OnShopRightInput -= Right;
        _translator.OnShopUpInput -= Up;
        _translator.OnShopDownInput -= Down;
        _translator.OnPurchaseEvent -= Purchase;
    }

    private void Left()
    {
        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().DecreaseAmount();
    }
    private void Right()
    {
        //PlayerStats.UpdateCurrency(100);
        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().IncreaseAmount();
    }
    private void Down()
    {
        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().ToggleHighlight(false);
        currentItemIndex++;
        if (currentItemIndex >= ShopItemArray.Length) 
        {
            currentItemIndex = 0;
        }
        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().ToggleHighlight(true);
    }
    private void Up()
    {
        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().ToggleHighlight(false);
        currentItemIndex--;
        if (currentItemIndex < 0)
        {
            currentItemIndex = ShopItemArray.Length - 1;
        }
        ShopItemArray[currentItemIndex].GetComponent<ShopItem>().ToggleHighlight(true);
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

        if(totalCost <= PlayerCurrencySystem.Instance.GetGoldCurrency())
        {
            totalCostText.color = Color.white;
        }
        else
        {
            totalCostText.color = Color.red;
        }

        currentGoldText.text = $"Gold Balance: ${PlayerCurrencySystem.Instance.GetGoldCurrency().ToString()}";
    }

    public void Purchase()
    {
        if (totalCost <= 0) return;
        if(PlayerCurrencySystem.Instance.GetGoldCurrency() >= totalCost)
        {
            foreach (GameObject item in ShopItemArray)
            {
                ShopItem shopItemScript = item.GetComponent<ShopItem>();
                for (int i = 0; i < shopItemScript.GetAmount(); i++)
                {
                    Instantiate(shopItemScript.GetPrefab(), PlayerRef.Transform.position, Quaternion.identity);
                }
                shopItemScript.ResetAmount();
            }

            PlayerCurrencySystem.Instance.DecrementGoldCurrency((int)totalCost);
        }
    }
}
