using System;
using System.Collections;
using System.Threading.Tasks;
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

    public float CurrentHealth 
    {
        get => _currentHealth; 
        set
        {
            _currentHealth = value;
            if (healthChannel != null) healthChannel.Invoke(_currentHealth, _maxHealth);
        } 
    }
    public float CurrentArmor 
    { 
        get => _currentArmor;
        set 
        { 
            _currentArmor = value;
            if (armorChannel != null) armorChannel.Invoke(_currentArmor, _maxArmor);
        }
    }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float MaxArmor { get => _maxArmor; set => _maxArmor = value; }
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
    //--------------------
    // Health Regen
    //--------------------

    private float _regenHealthCooldown = 5f;
    private float _regenHealthTickTime = 10f;
    private float _regenHealthAmount = 0f;
    private bool _canRegenHealth = false;
    private Coroutine _regenHealthTickCoroutine;
    public float RegenHealthAmount { get => _regenHealthAmount; set => _regenHealthAmount = value; }
    public void EnableHealthRegen()
    {
        _canRegenHealth = true;
        _regenHealthTickCoroutine = StartCoroutine(RegenTickRate());
    }
    public void PauseHealthRegen()
    {
        _canRegenHealth = false;
        if (_regenHealthTickCoroutine != null)
        {
            StopCoroutine(_regenHealthTickCoroutine);
            StartCoroutine(RegenCooldown());
        }
    }
    private IEnumerator RegenCooldown() //After taking damage, how long should pass till health can regen once more.
    {
        yield return new WaitForSeconds(_regenHealthCooldown);
        EnableHealthRegen();
    }
    private IEnumerator RegenTickRate()
    {
        while (_canRegenHealth)
        {
            yield return new WaitForSeconds(_regenHealthTickTime);

            if (_canRegenHealth)
                HealHealth(_regenHealthAmount);
        }
    }

    //--------------------
    // Health and Armor
    //--------------------

    //ISSUE: This should be moved somewhere else, doesn't seem appropriate in HealthSystem, 
    //seems like it would be better in NPC Damageable so that the multiplier can be passed to damage already being done to enemy
    //Either that or having another system that handles debuffs/damage multipliers for enemies or player
    public void SetDamageMultiplier(float multiplier)
    {
        _damageMultiplier = multiplier;
    }
    public void ResetHealth()
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
    public void Damage(float damage, Color color)
    {
        if (HasArmor)
        {
            DamageArmor(damage, color);
        }
        else
        {
            DamageHealth(damage, color);
            PauseHealthRegen();
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
            DamageHealth(healthDamage, color);
        }

        if (armorChannel != null) armorChannel.Invoke(_currentArmor, _maxArmor);
    }
}