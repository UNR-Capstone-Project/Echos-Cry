using System.Collections;
using UnityEngine;

public class ComboStateMachine : MonoBehaviour
{
    void HandleLightAttack()
    {
        if (!_readyForAttackInput) return;

        _currentState = _currentState.HandleLightAttackTransition();

        _readyForAttackInput = false;
        StartCoroutine(InputCooldown());
    }
    void HandleHeavyAttack()
    {
        if(!_readyForAttackInput) return;

        _currentState = _currentState.HandleHeavyAttackTransition();

        _readyForAttackInput = false;
        StartCoroutine(InputCooldown());
    }

    private IEnumerator InputCooldown()
    {
        yield return new WaitForSeconds(_inputCooldownTimer);
        _readyForAttackInput = true;
    }
    private void Awake()
    {
        _comboStateCache = new ComboStateCache();
    }
    private void Start()
    {
        if(_inputTranslator == null) return;
        _inputTranslator.OnLightAttackEvent += HandleLightAttack;
        _inputTranslator.OnHeavyAttackEvent += HandleHeavyAttack;
    }
    private void OnDestroy()
    {
        if (_inputTranslator == null) return;
        _inputTranslator.OnLightAttackEvent -= HandleLightAttack;
        _inputTranslator.OnHeavyAttackEvent -= HandleHeavyAttack;
    }

    private ComboState _currentState = null;
    private ComboStateCache _comboStateCache;
    private bool _readyForAttackInput = true;
    [SerializeField] private float _inputCooldownTimer = 0.5f;
    [SerializeField] private InputTranslator _inputTranslator;
}
