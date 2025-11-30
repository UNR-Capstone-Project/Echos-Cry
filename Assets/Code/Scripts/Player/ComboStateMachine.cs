using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

//Provides Instance static variable that will allow it to be accessed so it can pass on necessary combo information
public class ComboState
{
    public ComboState(ComboStateMachine.StateName stateName)
    {
        StateName = stateName;
    }
    public ComboState NextLightAttack;
    public ComboState NextHeavyAttack;
    public ComboStateMachine.StateName StateName;
}

public class ComboStateMachine : MonoBehaviour
{
    public StateName HandleLightAttack()
    {
        StopAllCoroutines();

        if (_currentState.NextLightAttack == null) _currentState = _startState.NextLightAttack;
        else _currentState = _currentState.NextLightAttack;

        return _currentState.StateName;
    }
    public StateName HandleHeavyAttack()
    {
        StopAllCoroutines();

        if (_currentState.NextHeavyAttack == null) _currentState = _startState.NextHeavyAttack;
        else _currentState = _currentState.NextHeavyAttack;

        return _currentState.StateName;
    }

    public void StartComboResetTimer()
    {
        StartCoroutine(ComboResetTimer());
    }
    private IEnumerator ComboResetTimer()
    {
        yield return new WaitForSeconds(_comboResetTime);
        _currentState = _startState;
    }

    void InitializeComboTree()
    {
        ComboState[] comboArray =
        {
            new(StateName.LIGHT1),
            new(StateName.LIGHT2),
            new(StateName.LIGHT3),
            new(StateName.LIGHT4),
            new(StateName.LIGHT5),
            new(StateName.HEAVY1),
            new(StateName.HEAVY2),
            new(StateName.HEAVY3)
        };
        _startState = new(StateName.START);
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
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        InitializeComboTree();
    }
    private void Start()
    {
        _currentState = _startState;

        BaseWeapon.OnAttackEndedEvent += StartComboResetTimer;
    }
    private void OnDestroy()
    {
        BaseWeapon.OnAttackEndedEvent -= StartComboResetTimer;
    }

    public static ComboStateMachine Instance { get; private set; }

    public enum StateName
    {
        START = -1,
        LIGHT1, LIGHT2, LIGHT3, LIGHT4, LIGHT5,
        HEAVY1, HEAVY2, HEAVY3
    }

    [SerializeField] private float _comboResetTime = 0.5f;

    private ComboState   _currentState = null;
    private ComboState   _startState   = null;
    private ComboState[] _comboStates  = null;
}
