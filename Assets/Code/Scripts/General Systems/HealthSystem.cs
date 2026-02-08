using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Stats Configuration File")]
    [SerializeField] private StatsConfig _statsConfig;
    [Header("(Player Only) Event Channels")]
    [Tooltip("Only relevant for players right now!")]
    [SerializeField] DoubleFloatEventChannel healthChannel;
    [Tooltip("Only relevant for players right now!")]
    [SerializeField] DoubleFloatEventChannel armorChannel;

    private float _currentHealth;
    private float _currentArmor;
    private float _maxHealth;
    private float _maxArmor;
    private float _damageMultiplier = 1f;

    public float CurrentHealth { get => _currentHealth; }
    public float CurrentArmor { get => _currentArmor; }
    public float MaxHealth { get => _maxHealth; }
    public float MaxArmor { get => _maxArmor; }
    public bool HasArmor => _currentArmor > 0;
    public float DamageMultiplier => _damageMultiplier;

    private void Start()
    {
        _currentHealth = _statsConfig.maxHealth;
        _currentArmor = _statsConfig.maxArmor;
        _maxHealth = _statsConfig.maxHealth;
        _maxArmor = _statsConfig.maxArmor;

        //These are called to update the starting values of each bar.
        if (healthChannel != null)
            healthChannel.Invoke(_currentHealth, _maxHealth);
        if (armorChannel != null)
            armorChannel.Invoke(_currentArmor, _maxArmor);
    }

    public void SetDamageMultiplier(float multiplier)
    {
        _damageMultiplier = multiplier;
    }

    public void Damage(float damage, Color color)
    {
        if (HasArmor)
        {
            DamageArmor(damage, color);
        }
        else
        {
            DamageHealth(damage, color);
        }
    }
    public void HealHealth(float heal)
    {
        _currentHealth += Mathf.Abs(heal);
        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
        if(healthChannel != null) healthChannel.Invoke(_currentHealth, _maxHealth);
    }
    public void HealArmor(float heal)
    {
        _currentArmor += Mathf.Abs(heal);
        if (_currentArmor > _maxArmor) _currentArmor = _maxArmor;
        if (armorChannel != null) armorChannel.Invoke(_currentArmor, _maxArmor);
    }
    public void DamageHealth(float damage, Color color)
    {
        _currentHealth -= Mathf.Abs(damage);
        if(_currentHealth < 0) _currentHealth = 0;
        if (healthChannel != null) healthChannel.Invoke(_currentHealth, _maxHealth);
    }
    public void DamageArmor(float damage, Color color)
    {
        _currentArmor -= Mathf.Abs(damage);

        if (_currentArmor < 0) //If the enemies armor is depleted to a negative amount,
                               //the negative amount of armor will then be transferred to damage to their health
        {
            float healthDamage = _currentArmor;
            _currentArmor = 0;
            if (armorChannel != null) armorChannel.Invoke(_currentArmor, _maxArmor);
            DamageHealth(healthDamage, color);
        }
    }
}