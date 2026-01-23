using System;
using UnityEngine;

//Handles PlayerAttacks
//Manages players Heavy and Light attack inputs
//Determines if we are ready for a new attack and if it is on beat

public class WeaponHandler : MonoBehaviour
{
    public void HandleLightInput()
    {
        if (!_readyForAttackInput || !TempoConductor.Instance.IsOnBeat()) return;
        _readyForAttackInput = false;
        
    }
    public void HandleHeavyInput()
    {
        if (!_readyForAttackInput || !TempoConductor.Instance.IsOnBeat()) return;
        _readyForAttackInput = false;
        
    }
    void ResetAttackInput()
    {
        _readyForAttackInput = true;
    }

    private void Start()
    {
        _readyForAttackInput = true;
        
    }

    [SerializeField] private InputTranslator _inputTranslator;
    private bool _readyForAttackInput = true;
    public static event Action<ComboTree.StateName> OnInputRegisteredEvent;
}
