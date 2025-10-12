using System.Collections;
using UnityEngine;

public class ComboStateMachine : MonoBehaviour
{
    void HandleLightAttack()
    {
        if (!_readyForAttackInput) return;

        if (CurrentState.NextLightAttack == null) CurrentState = CurrentState.StartState.NextLightAttack;
        else CurrentState = CurrentState.NextLightAttack;

        InitiateAttack();

        _readyForAttackInput = false;
        StartCoroutine(TempInputReset());
    }
    void HandleHeavyAttack()
    {
        if(!_readyForAttackInput) return;

        if (CurrentState.NextHeavyAttack == null) CurrentState = CurrentState.StartState.NextHeavyAttack;
        else CurrentState = CurrentState.NextHeavyAttack;

        InitiateAttack();

        _readyForAttackInput = false;
        StartCoroutine(TempInputReset());
    }

    void InitiateAttack()
    {
        if (CurrentState == null)
        {
            CurrentState = CurrentState.StartState;
            return;
        }
        else CurrentState.InitiateComboState(_attackAnimator);
    }
    public void ReadyForNewInput()
    {
        _readyForAttackInput = true;
        StartCoroutine(ComboResetTimer());
    }
    void WeaponChange(ComboState newStart)
    {
        _startState = newStart;
        CurrentState = newStart;
    }

    private IEnumerator ComboResetTimer()
    {
        yield return new WaitForSeconds(_comboResetTime);
        CurrentState = _startState;
    }
    private IEnumerator TempInputReset()
    {
        yield return new WaitForSeconds(_tempInputResetTime);
        ReadyForNewInput();
    }

    private void Awake()
    {
        _attackAnimator = GetComponent<Animator>(); 
    }
    private void Start()
    {
        if(CurrentState == null || _inputTranslator == null) return;

        _startState = CurrentState;

        CurrentState.InitiateComboState(_attackAnimator);
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
    private ComboState _startState = null;
    private bool _readyForAttackInput = true;
    [SerializeField] private float _comboResetTime = 0.5f;
    [SerializeField] private float _tempInputResetTime = 0.5f;
    [SerializeField] private InputTranslator _inputTranslator;
    private Animator _attackAnimator;
}
