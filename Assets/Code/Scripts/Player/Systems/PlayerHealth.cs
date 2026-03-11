using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;

    [Header("(Player Only) Event Channels")]
    [Tooltip("Only relevant for players right now!")]
    [SerializeField] DoubleFloatEventChannel _healthChannel;
    [Tooltip("Only relevant for players right now!")]
    [SerializeField] DoubleFloatEventChannel _armorChannel;

    public bool HasArmor => _healthSystem.CurrentArmor > 0;

    private void OnEnable()
    {
        //These are called to update the starting values of each bar.
        if (_healthChannel != null)
            _healthChannel.Invoke(_healthSystem.CurrentHealth, _healthSystem.MaxHealth);
        if (_armorChannel != null)
            _armorChannel.Invoke(_healthSystem.CurrentArmor, _healthSystem.MaxArmor);
    }

    //--------------------
    // Health and Armor
    //--------------------

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
        _healthSystem.CurrentHealth += Mathf.Abs(heal);
        if (_healthSystem.CurrentHealth > _healthSystem.MaxHealth) _healthSystem.CurrentHealth = _healthSystem.MaxHealth;
        if (_healthChannel != null) _healthChannel.Invoke(_healthSystem.CurrentHealth, _healthSystem.MaxHealth);
    }
    public void HealArmor(float heal)
    {
        _healthSystem.CurrentArmor += Mathf.Abs(heal);
        if (_healthSystem.CurrentArmor > _healthSystem.MaxArmor) _healthSystem.CurrentArmor = _healthSystem.MaxArmor;
        if (_armorChannel != null) _armorChannel.Invoke(_healthSystem.CurrentArmor, _healthSystem.MaxArmor);
    }
    public void DamageHealth(float damage, Color color)
    {
        _healthSystem.CurrentHealth -= Mathf.Abs(damage);
        if (_healthSystem.CurrentHealth < 0) _healthSystem.CurrentHealth = 0;
        if (_healthChannel != null) _healthChannel.Invoke(_healthSystem.CurrentHealth, _healthSystem.MaxHealth);
    }
    public void DamageArmor(float damage, Color color)
    {
        _healthSystem.CurrentArmor -= Mathf.Abs(damage);

        if (_healthSystem.CurrentArmor < 0) 
        {
            float healthDamage = _healthSystem.CurrentArmor;
            _healthSystem.CurrentArmor = 0;
            DamageHealth(healthDamage, color);
        }

        if (_armorChannel != null) _armorChannel.Invoke(_healthSystem.CurrentArmor, _healthSystem.MaxArmor);
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
}
