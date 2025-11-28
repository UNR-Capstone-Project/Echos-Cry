using System;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    void HandleLightInput()
    {
        if (!_readyForAttackInput || TempoManager.CurrentHitQuality == TempoManager.HIT_QUALITY.MISS) return;
        _readyForAttackInput = false;
        OnAttackEvent?.Invoke(ComboStateMachine.Instance.HandleLightAttack());
    }
    void HandleHeavyInput()
    {
        if (!_readyForAttackInput || TempoManager.CurrentHitQuality == TempoManager.HIT_QUALITY.MISS) return;
        _readyForAttackInput = false;
        OnAttackEvent?.Invoke(ComboStateMachine.Instance.HandleHeavyAttack());
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

        Weapon.OnAttackEndedEvent += ResetAttackInput;
    }
    private void OnDestroy()
    {
        InputTranslator.OnLightAttackEvent -= HandleLightInput;
        InputTranslator.OnHeavyAttackEvent -= HandleHeavyInput;

        Weapon.OnAttackEndedEvent -= ResetAttackInput;
    }

    private bool _readyForAttackInput = true;
    public static event Action<ComboStateMachine.StateName> OnAttackEvent;
}
