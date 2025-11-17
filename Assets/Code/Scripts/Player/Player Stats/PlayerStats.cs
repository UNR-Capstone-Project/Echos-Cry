using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player Stats")]
public class PlayerStats : MonoBehaviour
{
    public void AddCurrency(int amount)
    {
        _currencyCount += amount;
    }

    public void subtractCurrency(int amount){
        _currencyCount -= amount;
    }

    public int GetCountAttacksHit()
    {
        return _attacksHitCount;
    }
    public void AddCountAttacksHit(int amount)
    {
        _attacksHitCount += amount;
    }

    public void OnDamageTaken(float damageAmount)
    {
        _currentHealth -= damageAmount;
        OnPlayerDamagedEvent?.Invoke();
        OnPlayerHealthChangeEvent?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth <= 0) OnPlayerDeathEvent?.Invoke();
    }
    public void OnDamageHealed(float healAmount)
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
    { //Start does not get called on scriptable objects!
        _instance = this;
        _currentHealth = _maxHealth;
        _currencyCount = 0;
        _attacksHitCount = 0;
    }

    private static PlayerStats _instance;
    public static PlayerStats Instance {  get { return _instance; } }   

    public static event Action OnPlayerDamagedEvent;
    public static event Action OnPlayerHealedEvent;
    public static event Action OnPlayerDeathEvent;
    public static event Action<float, float> OnPlayerHealthChangeEvent;

    [SerializeField] private float _maxHealth = 100f;
    private int _currencyCount = 0;
    private int _attacksHitCount = 0;
    public float MaxHealth { get { return _maxHealth; } }
    public int CurrencyCount {  get { return _currencyCount; } }

    private float _currentHealth;
    public float CurrentHealth { get { return _currentHealth; } }
}
