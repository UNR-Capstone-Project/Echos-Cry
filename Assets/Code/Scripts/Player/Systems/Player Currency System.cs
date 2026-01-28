using System;
using UnityEngine;


public class PlayerCurrencySystem : Singleton<PlayerCurrencySystem>
{

    public void IncrementFingerCurrency(int amount)
    {
        _fingerCurrency += amount;
        OnCurrencyChangeEvent?.Invoke(_fingerCurrency);
    }
    public void DecrementFingerCurrency(int amount)
    {
        _fingerCurrency -= amount;
        OnCurrencyChangeEvent?.Invoke(_fingerCurrency);
    }

    public void SetFingerCurrency(int amount)
    {
        _fingerCurrency = amount;
        OnCurrencyChangeEvent?.Invoke(_fingerCurrency);
    }

    public int GetFingerCurrency()
    {
        return _fingerCurrency;
    }

    private void Start()
    {
        if(_playerCurrencyConfig == null)
        {
            Debug.LogWarning("Player Currency Configuration file is null");
            return;
        }
        _fingerCurrency = _playerCurrencyConfig.StartingFingerCurrency;
    }

    [Header("Configuration Object")]
    [SerializeField] private PlayerCurrencyConfig _playerCurrencyConfig;

    private int _fingerCurrency;
    public int FingerCurrency { get { return _fingerCurrency; } }

    public static event Action<int> OnCurrencyChangeEvent;
}
