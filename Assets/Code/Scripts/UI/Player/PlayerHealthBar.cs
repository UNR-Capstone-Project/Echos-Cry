#if UNITY_EDITOR
using UnityEditor.Rendering;
using UnityEditor.Rendering.Universal.ShaderGUI;
#endif
using UnityEngine;
using UnityEngine.UI;
/// Original Author: Victor
/// All Contributors Since Creation: Victor
/// Last Modified By:
public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private DoubleFloatEventChannel eventChannel;
    [SerializeField] private TMPro.TextMeshProUGUI healthText;

    // Variables Provided from here: https://youtu.be/CFASjEuhyf4?si=ri_WpIV1OxgtQdgp at 5:00
    private float lerpTimer;
    public float chipSpeed = 3f;
    private float hFraction = 0f;
    [SerializeField] private UnityEngine.UI.Image frontHealthBar;
    [SerializeField] private UnityEngine.UI.Image backHealthBar;

    void Awake()
    {
        UpdateHealth(100, 100);
    }

    void Start()
    {
        if(eventChannel != null) eventChannel.Channel += UpdateHealth;
    }

    void OnDestroy()
    {
        if (eventChannel != null) eventChannel.Channel -= UpdateHealth;
    }

    private void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        hFraction = currentHealth / maxHealth;
        lerpTimer = 0f;
        
    }

    private void Update()
    {
        //Code Section Provided Here: https://youtu.be/CFASjEuhyf4?si=ri_WpIV1OxgtQdgp at 13:19
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }
}
