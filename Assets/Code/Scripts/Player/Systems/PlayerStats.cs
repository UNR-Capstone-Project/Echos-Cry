using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Configuration Object")]
    [SerializeField] private StatsConfig _playerStatsConfig;

    //TODO: change these so they don't exist at some point
    public static event Action OnPlayerDamagedEvent;
    public static event Action OnPlayerHealedEvent;
    public static event Action OnPlayerDeathEvent;
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

        CameraManager.Instance.ScreenShake(0.6f, 0.2f);

        //TODO: Change this so that there is a PlayerDeathState and checks happen in state machine
        if (_currentHealth == 0) OnPlayerDeathEvent?.Invoke();
    }
    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;
        if(_currentHealth > _playerStatsConfig.maxHealth) _currentHealth = _playerStatsConfig.maxHealth;

        OnPlayerHealedEvent?.Invoke();
        OnPlayerHealthChangeEvent?.Invoke(_currentHealth, _playerStatsConfig.maxHealth);
    }
}
