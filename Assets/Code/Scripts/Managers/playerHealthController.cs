using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class playerHealthController : MonoBehaviour
{
    public static event Action<float> onPlayerDamaged;
    public static event Action<float> onPlayerHealed;
    public static event Action<float, float> onPlayerHealthChange;
    public static event Action onPlayerDeath;

    [SerializeField] private float MAX_HEALTH = 100f;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = MAX_HEALTH;
    }

    private void Start()
    {
        onPlayerHealthChange?.Invoke(currentHealth, MAX_HEALTH);
    }

    public float getCurrentPlayerHealth()
    {
        return currentHealth;
    }

    public float getPlayerMaxHealth()
    {
        return MAX_HEALTH;
    }

    public void onDamageTaken(float damageAmount)
    {
        currentHealth -= damageAmount;
        onPlayerDamaged?.Invoke(damageAmount);
        onPlayerHealthChange?.Invoke(currentHealth, MAX_HEALTH);

        if (currentHealth <= 0)
        {
            onPlayerDeath?.Invoke();
        }
    }

    public void onDamageHealed(float healAmount)
    {
        currentHealth += healAmount;
        onPlayerHealed?.Invoke(healAmount);
        onPlayerHealthChange?.Invoke(currentHealth, MAX_HEALTH);

        if (currentHealth > MAX_HEALTH) //Clamp health
        {
            currentHealth = MAX_HEALTH;
        }

        
    }
    

}
