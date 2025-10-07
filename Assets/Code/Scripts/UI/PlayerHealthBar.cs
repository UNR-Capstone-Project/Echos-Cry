using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteFrames;
    [SerializeField] private UnityEngine.UI.Image mImage;
    [SerializeField] private TMPro.TextMeshProUGUI healthText;
    [SerializeField] private playerHealthController _playerHealthController;
    private int totalFrames;

    void Awake()
    {
        totalFrames = spriteFrames.Length;
        updateHealth(100, 100);
    }

    void Start()
    {
        _playerHealthController.OnPlayerHealthChangeEvent += updateHealth;
        _playerHealthController.OnPlayerDeathEvent += updateHealthOnDeath;
    }

    void OnDestroy()
    {
        _playerHealthController.OnPlayerHealthChangeEvent -= updateHealth;
        _playerHealthController.OnPlayerDeathEvent -= updateHealthOnDeath;
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
