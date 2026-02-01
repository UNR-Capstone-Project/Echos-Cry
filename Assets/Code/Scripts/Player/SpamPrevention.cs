using System.Collections;
using UnityEngine;

public class SpamPrevention : MonoBehaviour
{
    [SerializeField] private InputTranslator _translator;
    [SerializeField] private int _maxInputCount = 5;
    [SerializeField, Range(0f, 10f)] private float _inputCooldown;
    private int _currentInputCount = 0;

    public static bool InputLocked {  get; private set; }

    private void OnEnable()
    {
        _translator.OnPrimaryActionEvent += IncrementCount;
        _translator.OnSecondaryActionEvent += IncrementCount;
        _translator.OnDashEvent += IncrementCount;
    }
    private void Start()
    {
        StartCoroutine(ResetCountPerSecondCoroutine());
    }

    private void IncrementCount(bool isPressed)
    {
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
