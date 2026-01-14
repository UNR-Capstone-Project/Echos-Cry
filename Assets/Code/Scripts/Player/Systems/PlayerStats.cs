using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Configuration Object")]
    [SerializeField] private PlayerStatsConfig _playerStatsConfig;

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
        _currentHealth = _playerStatsConfig.MaxHealth.Value;
    }

    public void OnPlayerDamaged(float damageAmount)
    {
        //if (invulnerability) { return; }
        Debug.Assert(damageAmount >= 0);

        _currentHealth -= damageAmount;
        if (_currentHealth < 0) _currentHealth = 0;

        OnPlayerDamagedEvent?.Invoke();
        OnPlayerHealthChangeEvent?.Invoke(_currentHealth, _playerStatsConfig.MaxHealth.Value);
    }
    public void OnPlayerHealed(float healAmount)
    {
        Debug.Assert(healAmount >= 0);

        _currentHealth += healAmount;
        if(_currentHealth > _playerStatsConfig.MaxHealth.Value) _currentHealth = _playerStatsConfig.MaxHealth.Value;

        OnPlayerHealedEvent?.Invoke();
        OnPlayerHealthChangeEvent?.Invoke(_currentHealth, _playerStatsConfig.MaxHealth.Value);
    }
}
