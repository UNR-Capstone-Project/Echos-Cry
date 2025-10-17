using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using static TempoManagerV2;

public class ComboStateMachine : MonoBehaviour
{
    void HandleLightAttack()
    {
        if (!_readyForAttackInput) return;

        if (IsAttackMissed()) return;

        StopAllCoroutines();
        _readyForAttackInput = false;

        if (_currentState.NextLightAttack == null) _currentState = _startState.NextLightAttack;
        else _currentState = _currentState.NextLightAttack;

        _currentState.InitiateComboState(_attackAnimator);

        StartCoroutine(AnimationLengthWait());
    }
    void HandleHeavyAttack()
    {
        if (!_readyForAttackInput) return;

        if (IsAttackMissed()) return;

        StopAllCoroutines();
        _readyForAttackInput = false;

        if (_currentState.NextHeavyAttack == null) _currentState = _startState.NextHeavyAttack;
        else _currentState = _currentState.NextHeavyAttack;

        _currentState.InitiateComboState(_attackAnimator);

        StartCoroutine(AnimationLengthWait());
    }

    private bool IsAttackMissed()
    {
        TempoManagerV2.HIT_QUALITY hitQuality = tempoManager.UpdateHitQuality();
        return (hitQuality == HIT_QUALITY.MISS);
    }

    public void ResetInputState()
    {
        _attackAnimator.runtimeAnimatorController = _defaultRuntimeController;
        _attackAnimator.Play("Idle");
        _readyForAttackInput = true;
        StartCoroutine(ComboResetTimer());
    }
    private IEnumerator ComboResetTimer()
    {
        yield return new WaitForSeconds(_comboResetTime);
        _currentState = _startState;
        Debug.Log("Combo Reset");
  
    }
    private IEnumerator AnimationLengthWait()
    {
        yield return new WaitForSeconds(_currentState.AttackData.AnimationClip.length);
        ResetInputState();
    }

    //Update this eventually to take in a Weapon as a parameter and the weapon will store the Attack data
    //Initialize the combo tree and setup combo connections
    void InitializeComboTree()
    {
        ComboState[] comboArray =
        {
            //Eventually these will take values
            new ComboState(null, null, null), //Start state
            new ComboState(null, null, null), //Beginning of light attacks
            new ComboState(null, null, null),
            new ComboState(null, null, null),
            new ComboState(null, null, null),
            new ComboState(null, null, null),
            new ComboState(null, null, null), //Beginning of heavy attacks
            new ComboState(null, null, null),
            new ComboState(null, null, null)
        };
        _startState = comboArray[(int)SN.START];
        _startState.NextLightAttack = comboArray[(int)SN.LIGHT1];
        _startState.NextHeavyAttack = comboArray[(int)SN.HEAVY1];

        comboArray[(int)SN.LIGHT1].NextLightAttack = comboArray[(int)SN.LIGHT2];

        comboArray[(int)SN.LIGHT2].NextLightAttack = comboArray[(int)SN.LIGHT3];
        comboArray[(int)SN.LIGHT2].NextHeavyAttack = comboArray[(int)SN.HEAVY3];

        comboArray[(int)SN.HEAVY1].NextHeavyAttack = comboArray[(int)SN.HEAVY2];
        comboArray[(int)SN.HEAVY1].NextLightAttack = comboArray[(int)SN.LIGHT4];

        comboArray[(int)SN.LIGHT4].NextLightAttack = comboArray[(int)SN.LIGHT5];

        _comboStates = comboArray;
    }
    //An Attack attackDataArray should have an animation clip for each ComboState, except the start state, so its array size should be 1 less than the _comboStates size
    void InitializeComboAnimations(Attack[] attackDataArray)
    {
        if (_comboStates == null || attackDataArray == null || attackDataArray.Length != _comboStates.Length - 1) return;

        for (int i = 1; i < _comboStates.Length; i++)
        {
            _comboStates[i].AttackData = attackDataArray[i - 1];
        }
    }

    private void Awake()
    {
        _attackAnimator = GetComponentInChildren<Animator>();
        tempoManager = GameObject.Find("TempoManager").GetComponent<TempoManagerV2>();
        _defaultRuntimeController = _attackAnimator.runtimeAnimatorController;
        InitializeComboTree();
    }
    private void Start()
    {
        InitializeComboAnimations(_tempAttackArray);

        _currentState = _startState;

        _inputTranslator.OnLightAttackEvent += HandleLightAttack;
        _inputTranslator.OnHeavyAttackEvent += HandleHeavyAttack;
    }
    private void OnDestroy()
    {
        _inputTranslator.OnLightAttackEvent -= HandleLightAttack;
        _inputTranslator.OnHeavyAttackEvent -= HandleHeavyAttack;
    }

    //SN = StateName for easier reference
    enum SN
    {
        START = 0,
        LIGHT1, LIGHT2, LIGHT3, LIGHT4, LIGHT5,
        HEAVY1, HEAVY2, HEAVY3
    }
    [SerializeField] private InputTranslator _inputTranslator;
    [SerializeField] private float _comboResetTime = 0.5f;
    public Attack[] _tempAttackArray;
    private ComboState _currentState = null;
    private ComboState _startState = null;
    private ComboState[] _comboStates = null;
    private Animator _attackAnimator;
    private RuntimeAnimatorController _defaultRuntimeController;
    private bool _readyForAttackInput = true;

    private TempoManagerV2 tempoManager;
}
