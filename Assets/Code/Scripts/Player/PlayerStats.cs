using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public void Respawn()
    {
        _currentHealth = _maxHealth;
        OnPlayerHealthChangeEvent?.Invoke(_currentHealth, _maxHealth);
        StartInvulnerability();
    }

    private void StartInvulnerability()
    {
        StartCoroutine(InvulnerabilityTimer());
    }

    private IEnumerator InvulnerabilityTimer()
    {
        invulnerability = true;
        yield return new WaitForSeconds(2f);
        invulnerability = false;
    }

    public static void UpdateCurrency(int amount)
    {
        _currencyCount += amount;
        OnCurrencyChangeEvent?.Invoke();
    }

    public void OnDamageTaken(float damageAmount)
    {
        if (invulnerability) { return; }
        _currentHealth = Mathf.Clamp(_currentHealth - damageAmount, 0, _maxHealth);

        OnPlayerDamagedEvent?.Invoke();
        OnPlayerHealthChangeEvent?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth == 0) OnPlayerDeathEvent?.Invoke();
    }
    public static void OnDamageHealed(float healAmount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth += healAmount, 0, _maxHealth);
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
    }

    private static PlayerStats _instance;
    public static PlayerStats Instance {  get { return _instance; } }   

    public static event Action               OnPlayerDamagedEvent;
    public static event Action               OnPlayerHealedEvent;
    public static event Action               OnPlayerDeathEvent;
    public static event Action               OnCurrencyChangeEvent;
    public static event Action<float, float> OnPlayerHealthChangeEvent;

    private static float _maxHealth = 100f;
    private static int   _currencyCount;
    private static float _currentHealth;
    private bool invulnerability = false;

    public static float MaxHealth { get { return _maxHealth; } }
    public static int   CurrencyCount {  get { return _currencyCount; } }
    public static float CurrentHealth { get { return _currentHealth; } }

}
