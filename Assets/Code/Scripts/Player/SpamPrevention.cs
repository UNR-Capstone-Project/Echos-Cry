using System;
using System.Collections;
using UnityEngine;

public class SpamPrevention : MonoBehaviour
{
    [SerializeField] private InputTranslator _translator;
    [SerializeField] private int _maxInputCount = 5;
    [SerializeField, Range(0f, 10f)] private float _inputCooldown;
    private int _currentInputCount = 0;

    public static bool InputLocked { get; private set; }

    public int MaxInputCount {  get { return _maxInputCount; } }
    public float InputCooldown { get => _inputCooldown; }
    public int CurrentInputCount { get =>  _currentInputCount; }

    private void OnEnable()
    {
        _translator.OnPrimaryActionEvent += IncrementCount;
        _translator.OnSecondaryActionEvent += IncrementCount;
        _translator.OnDashEvent += IncrementCount;

        StartCoroutine(ResetCountPerSecondCoroutine());
    }
    private void OnDisable()
    {
        _translator.OnPrimaryActionEvent -= IncrementCount;
        _translator.OnSecondaryActionEvent -= IncrementCount;
        _translator.OnDashEvent -= IncrementCount;

        StopAllCoroutines();
    }

    public void IncrementCount(bool isPressed)
    {
        if (!isPressed) return;
        if (InputLocked) return;
        
        _currentInputCount++;
        if(_currentInputCount >= _maxInputCount)
        {
            InputLocked = true;
            StopAllCoroutines();
            StartCoroutine(InputCooldownCoroutine());
        }
    }
    private IEnumerator InputCooldownCoroutine()
    {
        yield return new WaitForSeconds(_inputCooldown);

        InputLocked = false;
        _currentInputCount = 0;
        StartCoroutine(ResetCountPerSecondCoroutine());
    }
    private IEnumerator ResetCountPerSecondCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _currentInputCount = 0;
        StartCoroutine(ResetCountPerSecondCoroutine());
    }
}
