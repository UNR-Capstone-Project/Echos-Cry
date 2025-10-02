using System.Linq;
using System.Security.Cryptography;
using Microsoft.Unity.VisualStudio.Editor;
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
    }

    void Start()
    {
        playerHealthController.onPlayerHealthChange += updateHealth;
        playerHealthController.onPlayerDeath += updateHealthOnDeath;
    }

    void OnDestroy()
    {
        playerHealthController.onPlayerHealthChange -= updateHealth;
        playerHealthController.onPlayerDeath -= updateHealthOnDeath;
    }

    private void updateHealth(float currentHealth, float maxHealth)
    {
        healthText.text = currentHealth.ToString();
        float percentHealth = currentHealth / maxHealth;
        int healthBarFrame = (int)Mathf.Ceil(totalFrames * percentHealth) - 1;
        //Debug.Log(currentHealth);
        //Debug.Log(healthBarFrame);

        mImage.sprite = spriteFrames[healthBarFrame];
    }

    private void updateHealthOnDeath() {
        
    }
}
