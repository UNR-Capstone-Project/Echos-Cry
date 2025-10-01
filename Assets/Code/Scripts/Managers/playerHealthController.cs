using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class playerHealthController : MonoBehaviour
{
    public static event Action<float> onPlayerDamaged;
    public static event Action<float> onPlayerHealed;
    public static event Action<float> onPlayerHealthChange;
    public static event Action onPlayerDeath;

    [SerializeField] private float maxHealth = 17f;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public float getCurrentPlayerHealth()
    {
        return currentHealth;
    }

    public float getPlayerMaxHealth()
    {
        return maxHealth;
    }

    public void onDamageTaken(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Damage has been taken by the player: now health is at: " + currentHealth);
        onPlayerDamaged?.Invoke(damageAmount);
        onPlayerHealthChange?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            onPlayerDeath?.Invoke();
        }

        
    }

    public void onDamageHealed(float healAmount)
    {
        currentHealth += healAmount;
        onPlayerHealed?.Invoke(healAmount);
        onPlayerHealthChange?.Invoke(currentHealth);

        if (currentHealth > 17)
        {
            currentHealth = 17;
        }

        
    }
    

}
