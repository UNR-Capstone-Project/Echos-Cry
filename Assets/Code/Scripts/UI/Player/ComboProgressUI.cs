using UnityEngine;
using UnityEngine.UI;

public class ComboProgressUI : MonoBehaviour
{
    [SerializeField] private Image progressRingImage;

    public void UpdateComboMeterFill(float currentAmount, float maxAmount)
    {
        progressRingImage.fillAmount = currentAmount / maxAmount;
    }

    private void Start()
    {
        PlayerComboMeter.OnComboMeterChangeEvent += UpdateComboMeterFill;
        UpdateComboMeterFill(0f, 1f); //Reset the combo meter fill to empty at the start of the game.
    }
    private void OnDestroy()
    {
        PlayerComboMeter.OnComboMeterChangeEvent -= UpdateComboMeterFill;
    }
}
