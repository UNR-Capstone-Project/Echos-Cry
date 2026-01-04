using TMPro;
using UnityEngine;

public class CurrencyCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;

    public void UpdateCurrencyText()
    {
        currencyText.text = "Fingers: 0";// + PlayerStats.CurrencyCount.ToString();
    }

    private void Start()
    {
       // PlayerStats.OnCurrencyChangeEvent += UpdateCurrencyText;
    }
    private void OnDestroy()
    {
       // PlayerStats.OnCurrencyChangeEvent -= UpdateCurrencyText;
    }
}
