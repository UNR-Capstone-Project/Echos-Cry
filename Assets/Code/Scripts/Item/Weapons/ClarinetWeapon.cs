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
        base.Attack(attackState);
        
        int attackIndex = (int)attackState;

        SetupCurrentDamage(_attackData[attackIndex]);

        gameObject.SetActive(true);
        SetupAndStartAnimation(_attackData[attackIndex]);
        PlayerMovement.PlayerRigidbody.AddForce(onAttackPushForce * PlayerDirection.AimDirection.normalized, ForceMode.Impulse);
        StartCoroutine(WaitForAnimationLength(_attackData[attackIndex]));

        SetupAndUseSound(_attackData[attackIndex]);
    }

    private void SetupCurrentDamage(AttackData attackData)
    {
        float multiplier;
        Color multiplierColor;
        if (TempoManager.CurrentHitQuality == TempoManager.HIT_QUALITY.EXCELLENT)
        {
            multiplier = 1.25f;
            multiplierColor = Color.purple;
        }
        else
        {
            multiplier = 1.15f;
            multiplierColor = Color.white;
        }
        float damage = attackData.BaseDamage * multiplier;

        _weaponCollisionHandler.UpdateAttackDamage(damage, multiplierColor);
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
        PlayerAttackHandler.OnInputRegisteredEvent += Attack;
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        PlayerAttackHandler.OnInputRegisteredEvent -= Attack;
    }
    private void OnEnable()
    {
        PlayerAttackHandler.OnInputRegisteredEvent += Attack;
    }
    private void OnDisable()
    {
        PlayerAttackHandler.OnInputRegisteredEvent -= Attack;
    }

    private AttackCollisionHandler _weaponCollisionHandler;

    //Amount of force used to push player in direction of their attacks
    [SerializeField] private float onAttackPushForce = 5f;
}
