using UnityEditor.Rendering;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI healthText;
    private float lerpTimer;
    public float chipSpeed = 3f;
    private float hFraction = 0f;
    [SerializeField] private UnityEngine.UI.Image frontHealthBar;
    [SerializeField] private UnityEngine.UI.Image backHealthBar;

    void Awake()
    {
        updateHealth(100, 100);
    }

    void Start()
    {
        PlayerStats.OnPlayerHealthChangeEvent += updateHealth;
        PlayerStats.OnPlayerDeathEvent += updateHealthOnDeath;
    }

    void OnDestroy()
    {
        PlayerStats.OnPlayerHealthChangeEvent -= updateHealth;
        PlayerStats.OnPlayerDeathEvent -= updateHealthOnDeath;
    }

    private void updateHealth(float currentHealth, float maxHealth)
    {
        healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        hFraction = currentHealth / maxHealth;
        lerpTimer = 0f;

        
        
    }

    private void Update()
    {
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

    private void updateHealthOnDeath() {
        
    }
}
