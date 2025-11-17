using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static void UpdateCurrency(int amount)
    {
        _currencyCount += amount;
        OnCurrencyChangeEvent?.Invoke();
    }

    public void subtractCurrency(int amount){
        _currencyCount -= amount;
    }

    public int GetCountAttacksHit()
    public static void UpdateComboMeter(float amount)
    {
        _comboMeterAmount = Mathf.Clamp(_comboMeterAmount += amount, 0, _comboMeterMax);
        OnComboMeterChangeEvent?.Invoke(_comboMeterAmount, _comboMeterMax);
    }

    public static void OnDamageTaken(float damageAmount)
    {
        _currentHealth -= damageAmount;
        if(_currentHealth < 0 ) _currentHealth = 0;

        OnPlayerDamagedEvent?.Invoke();
        OnPlayerHealthChangeEvent?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth == 0) OnPlayerDeathEvent?.Invoke();
    }
    public static void OnDamageHealed(float healAmount)
    {
        _currentHealth += healAmount;
        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
 
        OnPlayerHealedEvent?.Invoke();
        OnPlayerHealthChangeEvent?.Invoke(_currentHealth, _maxHealth);
    }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
        _currencyCount = 0;
        _comboMeterAmount = 0;
    }

    private static PlayerStats _instance;
    public static PlayerStats Instance {  get { return _instance; } }   

    public static event Action OnPlayerDamagedEvent;
    public static event Action OnPlayerHealedEvent;
    public static event Action OnPlayerDeathEvent;
    public static event Action<float, float> OnPlayerHealthChangeEvent;
    public static event Action OnCurrencyChangeEvent;
    public static event Action<float, float> OnComboMeterChangeEvent;

    [SerializeField] private static float _maxHealth = 100f;
    [SerializeField]private static float _comboMeterMax = 120f;
    private static int _currencyCount;
    private static float _currentHealth;
    private static float _comboMeterAmount;
    public static float MaxHealth { get { return _maxHealth; } }
    public static int   CurrencyCount {  get { return _currencyCount; } }
    public static float CurrentHealth { get { return _currentHealth; } }
    public static float ComboMeterAmount { get { return _comboMeterAmount; } }
}
