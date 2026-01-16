using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Configuration Object")]
    [SerializeField] private StatsConfig _playerStatsConfig;

    public static event Action OnPlayerDamagedEvent;
    public static event Action OnPlayerHealedEvent;
    public static event Action<float, float> OnPlayerHealthChangeEvent;

    private float _currentHealth;
    public float CurrentHealth { get { return _currentHealth; } }

    private void Start()
    {
        if (_playerStatsConfig == null)
        {
            Debug.LogWarning("Player Stats Configuration file is null!");
            return;
        }
        _currentHealth = _playerStatsConfig.maxHealth;
    }

    public void Damage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        if (_currentHealth < 0) _currentHealth = 0;

        OnPlayerDamagedEvent?.Invoke();
        OnPlayerHealthChangeEvent?.Invoke(_currentHealth, _playerStatsConfig.maxHealth);
    }
    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;
        if(_currentHealth > _playerStatsConfig.maxHealth) _currentHealth = _playerStatsConfig.maxHealth;

        OnPlayerHealedEvent?.Invoke();
        OnPlayerHealthChangeEvent?.Invoke(_currentHealth, _playerStatsConfig.maxHealth);
    }
}
