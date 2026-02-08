using UnityEngine;
using UnityEngine.UI;

public class ComboProgressUI : MonoBehaviour
{
    [SerializeField] private Image progressRingImage;
    [SerializeField] private Image combobarBackgroundFill;
    private Material combobarMaterial;
    [SerializeField] private Sprite[] combobarSprites;

    public void UpdateComboMeterFill(float currentAmount, float maxAmount)
    {
        progressRingImage.fillAmount = currentAmount / maxAmount;

        if (PlayerComboMeter.CurrentMeterState == PlayerComboMeter.MeterState.Starting)
        {
            combobarBackgroundFill.sprite = combobarSprites[0];
            combobarMaterial.SetTexture("_Texture2D", combobarSprites[0].texture);
        }
        if (PlayerComboMeter.CurrentMeterState == PlayerComboMeter.MeterState.OneThird)
        {
            combobarBackgroundFill.sprite = combobarSprites[1];
            combobarMaterial.SetTexture("_Texture2D", combobarSprites[1].texture);
        }
        else if (PlayerComboMeter.CurrentMeterState == PlayerComboMeter.MeterState.TwoThirds)
        {
            combobarBackgroundFill.sprite = combobarSprites[2];
            combobarMaterial.SetTexture("_Texture2D", combobarSprites[2].texture);
        }
        else if (PlayerComboMeter.CurrentMeterState == PlayerComboMeter.MeterState.Full)
        {
            combobarBackgroundFill.sprite = combobarSprites[3];
            combobarMaterial.SetTexture("_Texture2D", combobarSprites[3].texture);
        }
    }

    private void Start()
    {
        PlayerComboMeter.OnComboMeterChangeEvent += UpdateComboMeterFill;

        if (combobarBackgroundFill != null)
        {
            combobarMaterial = combobarBackgroundFill.material;
        }

        UpdateComboMeterFill(0f, 1f); //Reset the combo meter fill to empty at the start of the game.
    }
    private void OnDestroy()
    {
        PlayerComboMeter.OnComboMeterChangeEvent -= UpdateComboMeterFill;
    }
}
