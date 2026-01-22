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
            new(StateName.Light1),
            new(StateName.Light2),
            new(StateName.Light3),
            new(StateName.Light4),
            new(StateName.Light5),
            new(StateName.Heavy1),
            new(StateName.Heavy2),
            new(StateName.Heavy3)
        };
        _startState = new(StateName.Start);
        _startState.NextLightAttack = comboArray[(int)StateName.Light1];
        _startState.NextHeavyAttack = comboArray[(int)StateName.Heavy1];

        comboArray[(int)StateName.Light1].NextLightAttack = comboArray[(int)StateName.Light2];

        comboArray[(int)StateName.Light2].NextLightAttack = comboArray[(int)StateName.Light3];
        comboArray[(int)StateName.Light2].NextHeavyAttack = comboArray[(int)StateName.Heavy3];

        comboArray[(int)StateName.Heavy1].NextHeavyAttack = comboArray[(int)StateName.Heavy2];
        comboArray[(int)StateName.Heavy1].NextLightAttack = comboArray[(int)StateName.Light4];

        comboArray[(int)StateName.Light4].NextLightAttack = comboArray[(int)StateName.Light5];

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

        Weapon.OnAttackEndedEvent += StartComboResetTimer;
    }
    private void OnDestroy()
    {
        Weapon.OnAttackEndedEvent -= StartComboResetTimer;
    }

    public static ComboStateMachine Instance { get; private set; }

    public enum StateName
    {
        Start = -1,
        Light1, Light2, Light3, Light4, Light5,
        Heavy1, Heavy2, Heavy3
    }

    [SerializeField] private float _comboResetTime = 0.5f;

    private ComboState   _currentState = null;
    private ComboState   _startState   = null;
    private ComboState[] _comboStates  = null;
}
