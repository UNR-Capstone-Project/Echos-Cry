using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteFrames;
    [SerializeField] private UnityEngine.UI.Image mImage;
    [SerializeField] private TMPro.TextMeshProUGUI healthText;
    [SerializeField] private PlayerStats _playerStats;
    private int totalFrames;

    void Awake()
    {
        totalFrames = spriteFrames.Length;
        updateHealth(100, 100);
    }

    void Start()
    {
        _playerStats.OnPlayerHealthChangeEvent += updateHealth;
        _playerStats.OnPlayerDeathEvent += updateHealthOnDeath;
    }

    void OnDestroy()
    {
        _playerStats.OnPlayerHealthChangeEvent -= updateHealth;
        _playerStats.OnPlayerDeathEvent -= updateHealthOnDeath;
    }

    private void updateHealth(float currentHealth, float maxHealth)
    {
        healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();

        float percentHealth = currentHealth / maxHealth;
        int healthBarFrame = (int)Mathf.Ceil(totalFrames * percentHealth) - 1;

        mImage.sprite = spriteFrames[healthBarFrame];
    }

    private void updateHealthOnDeath() {
        
    }
}
