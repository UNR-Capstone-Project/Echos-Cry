using System.Collections;
using UnityEngine;

public class ComboStateMachine : MonoBehaviour
{
    void HandleLightAttack()
    {
        if (!_readyForAttackInput) return;

        if (CurrentState.NextLightAttack == null) CurrentState = CurrentState.StartState.NextLightAttack;
        else CurrentState = CurrentState.NextLightAttack;

        TryInitiateAttack();

        _readyForAttackInput = false;
        StartCoroutine(TempInputReset());
    }
    void HandleHeavyAttack()
    {
        if(!_readyForAttackInput) return;

        if (CurrentState.NextHeavyAttack == null) CurrentState = CurrentState.StartState.NextHeavyAttack;
        else CurrentState = CurrentState.NextHeavyAttack;

        TryInitiateAttack();

        _readyForAttackInput = false;
        StartCoroutine(TempInputReset());
    }
    void TryInitiateAttack()
    {
        if (CurrentState == null)
        {
            CurrentState = CurrentState.StartState;
            return;
        }
        else
        {
            StopAllCoroutines();
            CurrentState.InitiateComboState(_attackAnimator);
        }
    }

    private IEnumerator ComboResetTimer()
    {
        yield return new WaitForSeconds(_comboResetTime);
        CurrentState = _startState;
        Debug.Log("Combo Reset");
    }
    private IEnumerator TempInputReset()
    {
        yield return new WaitForSeconds(_tempInputResetTime);
        _readyForAttackInput = true;
        StartCoroutine(ComboResetTimer());
    }

    //public void ResetInput()
    //{
    //    _readyForAttackInput = true;
    //    StartCoroutine(ComboResetTimer());
    //}
    //void WeaponChange(ComboState newStart)
    //{
    //    _startState = newStart;
    //    CurrentState = newStart;
    //}
    private void Awake()
    {
        if(TryGetComponent<Animator>(out Animator animator)) _attackAnimator = animator;
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
