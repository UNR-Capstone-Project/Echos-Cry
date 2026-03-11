using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Stats Configuration File")]
    [SerializeField] private StatsConfig _statsConfig;

    private float _currentHealth;
    private float _currentArmor;
    private float _maxHealth;
    private float _maxArmor;

    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public float CurrentArmor { get => _currentArmor; set => _currentArmor = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float MaxArmor { get => _maxArmor; set => _maxArmor = value; }

    private void Start()
    {
        _currentHealth = _statsConfig.maxHealth;
        _currentArmor = _statsConfig.maxArmor;
        _maxHealth = _statsConfig.maxHealth;
        _maxArmor = _statsConfig.maxArmor;
    }
    public void ResetSystem()
    {
        _currentHealth = _statsConfig.maxHealth;
        _currentArmor = _statsConfig.maxArmor;
        _maxHealth = _statsConfig.maxHealth;
        _maxArmor = _statsConfig.maxArmor;
    }


    //TODO: Move somewhere else
    private float _damageMultiplier = 1f;
    public float DamageMultiplier => _damageMultiplier;

    //ISSUE: This should be moved somewhere else, doesn't seem appropriate in HealthSystem, 
    //seems like it would be better in NPC Damageable so that the multiplier can be passed to damage already being done to enemy
    //Either that or having another system that handles debuffs/damage multipliers for enemies or player
    public void SetDamageMultiplier(float multiplier)
    {
        _damageMultiplier = multiplier;
    }
}