using System;
using System.Collections;
using UnityEngine;
using static ComboStateMachine;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
    public void Attack(int attackIndex)
    {
        CurrentDamage = _attackData[attackIndex].BaseDamage;
        _attackAnimator.runtimeAnimatorController = _attackData[attackIndex].OverrideController;
        _attackAnimator.Play("Attack");
        SetupAndUseSound(_attackData[attackIndex]);
        StartCoroutine(WaitForAnimationLength(_attackData[attackIndex]));
    }
    private void SetupAndUseSound(AttackData attackData)
    {
        soundEffectManager.Instance.Builder
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

    public static float CurrentDamage = 0;

    private Animator                  _attackAnimator;
    private RuntimeAnimatorController _defaultAnimatorController;

    public static event Action OnAttackEndedEvent;
}
