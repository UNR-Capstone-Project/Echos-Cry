using UnityEngine;
using UnityEngine.UI;

public class ComboProgressUI : MonoBehaviour
{
    [SerializeField] private Image progressRingImage;

    public void UpdateComboMeterFill(float currentAmount, float maxAmount)
    {
        progressRingImage.fillAmount = currentAmount/maxAmount;
    }

    private void Start()
    {
        PlayerStats.OnComboMeterChangeEvent += UpdateComboMeterFill;
        progressRingImage.fillAmount = 0;
    }
    private void OnDestroy()
    {
        PlayerStats.OnComboMeterChangeEvent -= UpdateComboMeterFill;
    }
}
