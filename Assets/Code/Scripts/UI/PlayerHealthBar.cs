using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteFrames;
    [SerializeField] private UnityEngine.UI.Image mImage;
    [SerializeField] private TMPro.TextMeshProUGUI healthText;
    private int totalFrames;

    void Awake()
    {
        totalFrames = spriteFrames.Length;
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

        float percentHealth = currentHealth / maxHealth;
        int healthBarFrame = (int)Mathf.Ceil(totalFrames * percentHealth) - 1;
        if(healthBarFrame < 0) healthBarFrame = 0;

        mImage.sprite = spriteFrames[healthBarFrame];
    }

    private void updateHealthOnDeath() {
        
    }
}
