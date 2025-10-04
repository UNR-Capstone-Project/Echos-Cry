using System.Collections;
using UnityEngine;

public class ComboStateMachine : MonoBehaviour
{
    void HandleLightAttack()
    {
        if (!_readyForAttackInput) return;

        if (CurrentState.NextLightAttack == null) CurrentState = CurrentState.StartState.NextLightAttack;
        else CurrentState = CurrentState.NextLightAttack;

        if (CurrentState == null)
        {
            CurrentState = CurrentState.StartState; 
            return;
        }
        else CurrentState.InitiateComboState();

        _readyForAttackInput = false;
        StartCoroutine(InputCooldown());
    }
    void HandleHeavyAttack()
    {
        if(!_readyForAttackInput) return;

        if (CurrentState.NextHeavyAttack == null) CurrentState = CurrentState.StartState.NextHeavyAttack;
        else CurrentState = CurrentState.NextHeavyAttack;

        if (CurrentState == null)
        {
            CurrentState = CurrentState.StartState;
            return;
        }
        else CurrentState.InitiateComboState();

        _readyForAttackInput = false;
        StartCoroutine(InputCooldown());
    }

    private IEnumerator InputCooldown()
    {
        yield return new WaitForSeconds(_inputCooldownTimer);
        _readyForAttackInput = true;
    }

    private void Start()
    {
        if(CurrentState == null || _inputTranslator == null) return;
        _inputTranslator.OnLightAttackEvent += HandleLightAttack;
        _inputTranslator.OnHeavyAttackEvent += HandleHeavyAttack;
    }
    private void OnDestroy()
    {
        if (_inputTranslator == null || CurrentState == null) return;
        _inputTranslator.OnLightAttackEvent -= HandleLightAttack;
        _inputTranslator.OnHeavyAttackEvent -= HandleHeavyAttack;
    }

    public ComboState CurrentState = null;
    private bool _readyForAttackInput = true;
    [SerializeField] private float _inputCooldownTimer = 0.5f;
    [SerializeField] private InputTranslator _inputTranslator;
}
