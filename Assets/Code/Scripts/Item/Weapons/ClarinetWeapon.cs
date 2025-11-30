using System;
using System.Collections;
using UnityEngine;
using static ComboStateMachine;

[RequireComponent(typeof(Animator))]

//Updated so that the attack method will be tied to PlayerAttackHandler
//Weapon will message the PlayerAttackHandler and ComboStateMachine when it is finished with its attack

public class ClarinetWeapon : BaseWeapon
{
    protected override void Attack(StateName attackState)
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
        float damage = attackData.BaseDamage * multiplier;
        _weaponCollisionHandler.UpdateAttackDamage(damage);
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
        OnAttackEnded();
    }

    protected override void Awake()
    {
        base.Awake();
        _weaponCollisionHandler = GetComponentInChildren<AttackCollisionHandler>();
    }
    private void Start()
    {
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

    private AttackCollisionHandler _weaponCollisionHandler;
}
