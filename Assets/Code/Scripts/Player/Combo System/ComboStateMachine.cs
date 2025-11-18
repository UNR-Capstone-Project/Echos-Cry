using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public class ComboStateMachine : MonoBehaviour
{
    void HandleLightAttack()
    {
        if (!_readyForAttackInput || !IsOnBeat()) return;

        StopAllCoroutines();
        _readyForAttackInput = false;

        if (_currentState.NextLightAttack == null) _currentState = _startState.NextLightAttack;
        else _currentState = _currentState.NextLightAttack;

        equippedWeapon.SetActive(true);
        _currentState.InitiateComboState();
    }
    void HandleHeavyAttack()
    {
        if (!_readyForAttackInput || !IsOnBeat()) return;

        StopAllCoroutines();
        _readyForAttackInput = false;

        if (_currentState.NextHeavyAttack == null) _currentState = _startState.NextHeavyAttack;
        else _currentState = _currentState.NextHeavyAttack;

        equippedWeapon.SetActive(true);
        _currentState.InitiateComboState();
    }

    private bool IsOnBeat()
    {
        if ( TempoManager.UpdateHitQuality() == TempoManager.HIT_QUALITY.MISS) return false;
        else return true;
    }

    public void StartComboResetTimer()
    {
        _readyForAttackInput = true;
        equippedWeapon.SetActive(false);
        StartCoroutine(ComboResetTimer());
    }
    private IEnumerator ComboResetTimer()
    {
        yield return new WaitForSeconds(_comboResetTime);
        _currentState = _startState;
    }

    //Update this eventually to take in a Weapon as a parameter and the weapon will store the Attack data
    //Initialize the combo tree and setup combo connections
    void InitializeComboTree()
    {
        ComboState[] comboArray =
        {
            new ComboState(StateName.START),
            new ComboState(StateName.LIGHT1),
            new ComboState(StateName.LIGHT2),
            new ComboState(StateName.LIGHT3),
            new ComboState(StateName.LIGHT4),
            new ComboState(StateName.LIGHT5),
            new ComboState(StateName.HEAVY1),
            new ComboState(StateName.HEAVY2),
            new ComboState(StateName.HEAVY3)
        };
        _startState = comboArray[(int)StateName.START];
        _startState.NextLightAttack = comboArray[(int)StateName.LIGHT1];
        _startState.NextHeavyAttack = comboArray[(int)StateName.HEAVY1];

        comboArray[(int)StateName.LIGHT1].NextLightAttack = comboArray[(int)StateName.LIGHT2];

        comboArray[(int)StateName.LIGHT2].NextLightAttack = comboArray[(int)StateName.LIGHT3];
        comboArray[(int)StateName.LIGHT2].NextHeavyAttack = comboArray[(int)StateName.HEAVY3];

        comboArray[(int)StateName.HEAVY1].NextHeavyAttack = comboArray[(int)StateName.HEAVY2];
        comboArray[(int)StateName.HEAVY1].NextLightAttack = comboArray[(int)StateName.LIGHT4];

        comboArray[(int)StateName.LIGHT4].NextLightAttack = comboArray[(int)StateName.LIGHT5];

        _comboStates = comboArray;
    }

    private void Awake()
    {
        InitializeComboTree();
    }
    private void Start()
    {
        _currentState = _startState;

        InputTranslator.OnLightAttackEvent += HandleLightAttack;
        InputTranslator.OnHeavyAttackEvent += HandleHeavyAttack;

        Weapon.OnAttackEndedEvent += StartComboResetTimer;
    }
    private void OnDestroy()
    {
        InputTranslator.OnLightAttackEvent -= HandleLightAttack;
        InputTranslator.OnHeavyAttackEvent -= HandleHeavyAttack;

        Weapon.OnAttackEndedEvent -= StartComboResetTimer;
    }

    public enum StateName
    {
        START = 0,
        LIGHT1, LIGHT2, LIGHT3, LIGHT4, LIGHT5,
        HEAVY1, HEAVY2, HEAVY3
    }

    [SerializeField] private GameObject equippedWeapon;
    [SerializeField] private float _comboResetTime = 0.5f;

    private bool _readyForAttackInput = true;
    private ComboState _currentState = null;
    private ComboState _startState = null;
    private ComboState[] _comboStates = null;
}
