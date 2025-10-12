using System.Collections;
using UnityEngine;

public class ComboStateMachine : MonoBehaviour
{
    void HandleLightAttack()
    {
        if (!_readyForAttackInput) return;

        _readyForAttackInput = false;
        StopAllCoroutines();

        if (CurrentState.NextLightAttack == null) CurrentState = _startState.NextLightAttack;
        else CurrentState = CurrentState.NextLightAttack;

        CurrentState.InitiateComboState(_attackAnimator);
    }
    void HandleHeavyAttack()
    {
        if(!_readyForAttackInput) return;
        
        _readyForAttackInput = false;
        StopAllCoroutines();

        if (CurrentState.NextHeavyAttack == null) CurrentState = _startState.NextHeavyAttack;
        else CurrentState = CurrentState.NextHeavyAttack;

        CurrentState.InitiateComboState(_attackAnimator);
    }
    public void ResetInput()
    {
        Debug.Log("Reset Input Call");
        _attackAnimator.Play(Animator.StringToHash("Idle"));
        _readyForAttackInput = true;
        StartCoroutine(ComboResetTimer());
    }
    private IEnumerator ComboResetTimer()
    {
        yield return new WaitForSeconds(_comboResetTime);
        CurrentState = _startState;
        _attackAnimator.runtimeAnimatorController = _defaultRuntimeController;
        Debug.Log("Combo Reset");
    }
    //private IEnumerator AnimationLengthWait()
    //{
    //    yield return WaitForSeconds();
    //}

    private void Awake()
    {
        _attackAnimator = GetComponentInChildren<Animator>();
        _defaultRuntimeController = _attackAnimator.runtimeAnimatorController;
    }
    private void Start()
    {
        if(CurrentState == null || _inputTranslator == null) return;

        _startState = CurrentState;

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
    [SerializeField] private InputTranslator _inputTranslator;
    private Animator _attackAnimator;
    private RuntimeAnimatorController _defaultRuntimeController;
    private AnimationClip _animationClip;
}
