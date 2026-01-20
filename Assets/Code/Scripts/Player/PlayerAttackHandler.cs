using System;
using UnityEngine;

//Handles PlayerAttacks
//Manages players Heavy and Light attack inputs
//Determines if we are ready for a new attack and if it is on beat

public class PlayerAttackHandler : MonoBehaviour
{
    void HandleLightInput()
    {
        if (!_readyForAttackInput || !TempoConductor.Instance.IsOnBeat()) return;
        _readyForAttackInput = false;
        OnInputRegisteredEvent?.Invoke(ComboStateMachine.Instance.HandleLightAttack());
    }
    void HandleHeavyInput()
    {
        if (!_readyForAttackInput || !TempoConductor.Instance.IsOnBeat()) return;
        _readyForAttackInput = false;
        OnInputRegisteredEvent?.Invoke(ComboStateMachine.Instance.HandleHeavyAttack());
    }
    void ResetAttackInput()
    {
        _readyForAttackInput = true;
    }

    private void Start()
    {
        _readyForAttackInput = true;
        
        //_inputTranslator.OnLightAttackEvent += HandleLightInput;
        //_inputTranslator.OnHeavyAttackEvent += HandleHeavyInput;

        BaseWeapon.OnAttackEndedEvent += ResetAttackInput;
    }
    private void OnDestroy()
    {
        //_inputTranslator.OnLightAttackEvent -= HandleLightInput;
        //_inputTranslator.OnHeavyAttackEvent -= HandleHeavyInput;

        BaseWeapon.OnAttackEndedEvent -= ResetAttackInput;
    }

    [SerializeField] private InputTranslator _inputTranslator;
    private bool _readyForAttackInput = true;
    public static event Action<ComboStateMachine.StateName> OnInputRegisteredEvent;
}
