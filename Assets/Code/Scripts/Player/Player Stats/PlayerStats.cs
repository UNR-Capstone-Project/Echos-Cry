using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public event Action OnPlayerDamagedEvent;
    public event Action OnPlayerHealedEvent;
    public event Action<float, float> OnPlayerHealthChangeEvent;
    public event Action OnPlayerDeathEvent;

    [SerializeField] private float MAX_HEALTH = 100f;
    [SerializeField] private int currencyCount = 0;
    public float MaxHealth { get { return MAX_HEALTH; } }
    public int CurrencyCount {  get { return currencyCount; } }

    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } }

    private void Awake()
    {
        currentHealth = MAX_HEALTH;
    }

    public void AddCurrency(int amount)
    {
        currencyCount += amount;
    }

    public void OnDamageTaken(float damageAmount)
    {
        currentHealth -= damageAmount;
        OnPlayerDamagedEvent?.Invoke();
        OnPlayerHealthChangeEvent?.Invoke(currentHealth, MAX_HEALTH);

        if (currentHealth <= 0) OnPlayerDeathEvent?.Invoke();
    }

    public void OnDamageHealed(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > MAX_HEALTH) currentHealth = MAX_HEALTH;
 
        OnPlayerHealedEvent?.Invoke();
        OnPlayerHealthChangeEvent?.Invoke(currentHealth, MAX_HEALTH);
    }
}
