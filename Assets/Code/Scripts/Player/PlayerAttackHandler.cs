using System;
using UnityEngine;

//Handles PlayerAttacks
//Manages players Heavy and Light attack inputs
//Determines if we are ready for a new attack and if it is on beat

public class PlayerAttackHandler : MonoBehaviour
{
    void HandleLightInput()
    {
        if (!_readyForAttackInput || TempoManager.CurrentHitQuality == TempoManager.HIT_QUALITY.MISS) return;
        _readyForAttackInput = false;
        OnAttackStartEvent?.Invoke(ComboStateMachine.Instance.HandleLightAttack());
    }
    void HandleHeavyInput()
    {
        if (!_readyForAttackInput || TempoManager.CurrentHitQuality == TempoManager.HIT_QUALITY.MISS) return;
        _readyForAttackInput = false;
        OnAttackStartEvent?.Invoke(ComboStateMachine.Instance.HandleHeavyAttack());
    }
    void ResetAttackInput()
    {
        _readyForAttackInput = true;
    }

    private void Start()
    {
        _readyForAttackInput = true;
        
        InputTranslator.OnLightAttackEvent += HandleLightInput;
        InputTranslator.OnHeavyAttackEvent += HandleHeavyInput;

        BaseWeapon.OnAttackEndedEvent += ResetAttackInput;
    }
    private void OnDestroy()
    {
        InputTranslator.OnLightAttackEvent -= HandleLightInput;
        InputTranslator.OnHeavyAttackEvent -= HandleHeavyInput;

        BaseWeapon.OnAttackEndedEvent -= ResetAttackInput;
    }

    private bool _readyForAttackInput = true;
    public static event Action<ComboStateMachine.StateName> OnAttackStartEvent;
}
