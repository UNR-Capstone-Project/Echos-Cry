using System;
using System.Collections;
using UnityEngine;
using static ComboStateMachine;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
    public void Attack(StateName attackState)
    {
        int attackIndex = (int)attackState;

        SetupCurrentDamage(_attackData[attackIndex]);

        gameObject.SetActive(true);
        SetupAndStartAnimation(_attackData[attackIndex]);
        StartCoroutine(WaitForAnimationLength(_attackData[attackIndex]));

        SetupAndUseSound(_attackData[attackIndex]);
    }

    private void SetupCurrentDamage(AttackData attackData)
    {
        float multiplier;
        if (TempoManager.CurrentHitQuality == TempoManager.HIT_QUALITY.EXCELLENT) multiplier = 1.25f;
        else multiplier = 1.15f;
        CurrentDamage = attackData.BaseDamage * multiplier;
    }

    private void SetupAndUseSound(AttackData attackData)
    {
        soundEffectManager.Instance.Builder
            .setSound(attackData.AttackSound)
            .setSoundPosition(this.transform.position)
            .ValidateAndPlaySound();
    }
    private void SetupAndStartAnimation(AttackData attackData)
    {
        _attackAnimator.runtimeAnimatorController = attackData.OverrideController;
        _attackAnimator.Play("Attack");
    }
    private void ResetAnimation()
    {
        _attackAnimator.runtimeAnimatorController = _defaultAnimatorController;
        _attackAnimator.Play("Idle");
    }
    private IEnumerator WaitForAnimationLength(AttackData attackData)
    {
        yield return new WaitForSeconds(attackData.AnimationClip.length);
        ResetAnimation();
        gameObject.SetActive(false);
        OnAttackEndedEvent?.Invoke();
    }

    private void Awake()
    {
        _attackAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        _defaultAnimatorController = _attackAnimator.runtimeAnimatorController;
        PlayerAttackHandler.OnAttackEvent += Attack;
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        PlayerAttackHandler.OnAttackEvent -= Attack;
    }
    private void OnEnable()
    {
        PlayerAttackHandler.OnAttackEvent += Attack;
    }
    private void OnDisable()
    {
        PlayerAttackHandler.OnAttackEvent -= Attack;
    }

    //Must only be of the same length - 1 of the StateName enum
    [SerializeField] private AttackData[] _attackData;

    public static float CurrentDamage = 0;

    private Animator                  _attackAnimator;
    private RuntimeAnimatorController _defaultAnimatorController;

    public static event Action OnAttackEndedEvent;
}
