using System;
using UnityEngine;


public class PlayerCurrencySystem : MonoBehaviour
{
    public void IncrementFingerCurrency(int amount)
    {
        _fingerCurrency += amount;
        OnCurrencyChangeEvent?.Invoke(_fingerCurrency);
    }
    public void DecrementFingerCurrenct(int amount)
    {
        _fingerCurrency -= amount;
        OnCurrencyChangeEvent?.Invoke(_fingerCurrency);
    }

    private void Start()
    {
        _fingerCurrency = _playerCurrencyConfig.StartingFingerCurrency;
    }

    [Header("Configuration Object")]
    [SerializeField] private PlayerCurrencyConfig _playerCurrencyConfig;

    private int _fingerCurrency;
    public int FingerCurrency { get { return _fingerCurrency; } }

    public static event Action<int> OnCurrencyChangeEvent;
}
