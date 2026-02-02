using TMPro;
using UnityEngine;

public class CurrencyCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;

    public void UpdateCurrencyText(int currencyCount)
    {
        currencyText.text = "Gold: " + currencyCount.ToString();
    }

    private void Start()
    {
        PlayerCurrencySystem.OnCurrencyChangeEvent += UpdateCurrencyText;
    }
    private void OnDestroy()
    {
       PlayerCurrencySystem.OnCurrencyChangeEvent -= UpdateCurrencyText;
    }
}
