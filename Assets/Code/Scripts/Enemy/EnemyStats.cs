using AudioSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//Handles enemy stats such as health, armor etc
//Contains relevant functions and events for each stat/event

public class EnemyStats : MonoBehaviour
{
    [Header("Stats Configuration File")]
    [SerializeField] private StatsConfig _statsConfig;

    private float _health;
    private float _armor;
    private float _maxHealth;
    private float _maxArmor;

    public float Health { get => _health; }
    public float Armor { get => _armor; }
    public float MaxHealth { get => _maxHealth; }
    public float MaxArmor { get => _maxArmor; }

    public event Action<float, Color> OnEnemyDamagedEvent;
    public event Action OnEnemyHealedEvent;

    private void Start()
    {
        _health = _statsConfig.maxHealth;
        _armor = _statsConfig.maxArmor;
        _maxHealth = _statsConfig.maxHealth;
        _maxArmor = _statsConfig.maxArmor;
    }
 
    public void HealHealth(float heal)
    {
        _health += Mathf.Abs(heal);
        if (Health > MaxHealth) _health = MaxHealth;
        OnEnemyHealedEvent?.Invoke();
    }
    public void HealArmor(float heal)
    {
        _armor += Mathf.Abs(heal);
        if (Health > MaxHealth) _health = MaxHealth;
        OnEnemyHealedEvent?.Invoke();
    }
    public void DamageHealth(float damage, Color color)
    {
        _health -= Mathf.Abs(damage);
        OnEnemyDamagedEvent?.Invoke(damage, color);
    }
    public void DamageArmor(float damage, Color color)
    {
        _armor -= Mathf.Abs(damage);
        OnEnemyDamagedEvent?.Invoke(damage, color);
    }
}
