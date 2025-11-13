using TMPro;
using UnityEngine;

public class CurrencyCounterUI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private TextMeshProUGUI currencyText;

    void Update()
    {
        currencyText.text = "Fingers: " + playerStats.CurrencyCount.ToString();
    }
}
