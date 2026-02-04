using UnityEngine;

public class PlayerArmorBar : MonoBehaviour
{
    [SerializeField] private DoubleFloatEventChannel eventChannel;
    [SerializeField] private TMPro.TextMeshProUGUI armorText;

    // Variables Provided from here: https://youtu.be/CFASjEuhyf4?si=ri_WpIV1OxgtQdgp at 5:00
    private float lerpTimer;
    public float chipSpeed = 3f;
    private float hFraction = 0f;
    [SerializeField] private UnityEngine.UI.Image frontFillBar;
    [SerializeField] private UnityEngine.UI.Image backFillBar;

    void Start()
    {
        if (eventChannel != null) eventChannel.Channel += UpdateArmor;
    }

    void OnDestroy()
    {
        if (eventChannel != null) eventChannel.Channel -= UpdateArmor;
    }

    private void UpdateArmor(float currentArmor, float maxArmor)
    {
        armorText.text = currentArmor.ToString() + "/" + maxArmor.ToString();
        hFraction = currentArmor / maxArmor;
        lerpTimer = 0f;
    }

    private void Update()
    {
        //Code Section Provided Here: https://youtu.be/CFASjEuhyf4?si=ri_WpIV1OxgtQdgp at 13:19
        float fillF = frontFillBar.fillAmount;
        float fillB = backFillBar.fillAmount;

        if (fillB > hFraction)
        {
            frontFillBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backFillBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backFillBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontFillBar.fillAmount = Mathf.Lerp(fillF, backFillBar.fillAmount, percentComplete);
        }
    }
}