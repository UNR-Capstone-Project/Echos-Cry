using AudioSystem;
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
        PlayerMovement.PlayerRigidbody.AddForce(onAttackPushForce * PlayerDirection.AimDirection.normalized, ForceMode.VelocityChange);
        StartCoroutine(WaitForAnimationLength(_attackData[attackIndex]));

        attackSFX = _attackData[attackIndex].AttackSound;
    }

    private void SetupCurrentDamage(AttackData attackData)
    {
        float multiplier;
        Color multiplierColor;
        if (TempoManager.CurrentHitQuality == TempoManager.HIT_QUALITY.EXCELLENT)
        {
            multiplier = 1.25f;
            multiplierColor = new Color(110f / 255f, 44f / 255f, 222f / 255f, 1f); //purple
        }
        else
        {
            multiplier = 1.15f;
            multiplierColor = new Color(47f / 255f, 235f / 255f, 81f / 255f, 1.0f); //green
        }
        float damage = attackData.BaseDamage * multiplier;

        _weaponCollisionHandler.UpdateAttackDamage(damage, multiplierColor);
    }

    private void SetupAndUseSound()
    {
        soundEffectManager.Instance.Builder
            .setSound(attackSFX)
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
    private soundEffect attackSFX;

    //Amount of force used to push player in direction of their attacks
    [SerializeField] private float onAttackPushForce = 5f;
}
