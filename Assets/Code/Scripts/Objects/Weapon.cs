using System;
using System.Collections;
using UnityEngine;
using static ComboStateMachine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BaseAttack))]
public class Weapon : MonoBehaviour
{
    public void Attack(int attackIndex)
    {
        _attackAnimator.runtimeAnimatorController = _attackData[attackIndex].OverrideController;
        _attackAnimator.Play("Attack");
        _attackMethod.StartAttack(_attackData[attackIndex]);
        SetupAndUseSound(_attackData[attackIndex]);
        StartCoroutine(WaitForAnimationLength(_attackData[attackIndex]));
    }
    private void SetupAndUseSound(AttackData attackData)
    {
        soundEffectManager.Instance.createSound()
            .setSound(attackData.AttackSound)
            .setSoundPosition(this.transform.position)
            .ValidateAndPlaySound();
    }
    private IEnumerator WaitForAnimationLength(AttackData attackData)
    {
        yield return new WaitForSeconds(attackData.AnimationClip.length);
        _attackAnimator.runtimeAnimatorController = _defaultAnimatorController;
        _attackAnimator.Play("Idle");
        OnAttackEndedEvent?.Invoke();
    }

    private void Awake()
    {
        _attackAnimator = GetComponent<Animator>();
        _attackMethod = GetComponent<BaseAttack>();
    }
    private void Start()
    {
        _defaultAnimatorController = _attackAnimator.runtimeAnimatorController;
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        ComboState.OnInitiateComboEvent -= Attack;
    }
    private void OnEnable()
    {
        ComboState.OnInitiateComboEvent += Attack;
    }
    private void OnDisable()
    {
        ComboState.OnInitiateComboEvent -= Attack;
    }

    [SerializeField] private AttackData[] _attackData;

    private Animator                  _attackAnimator;
    private RuntimeAnimatorController _defaultAnimatorController;
    private BaseAttack                _attackMethod;

    public static event Action OnAttackEndedEvent;
}
