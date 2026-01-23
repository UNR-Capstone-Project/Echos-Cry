using UnityEngine;

public class ComboWeapon : Weapon
{
    private ComboTree _comboTree;
    [SerializeField] private AttackData[] _attackData;

    protected override void Attack()
    {
        //Stop Coroutines, specifically ComboResetTimer
        StopAllCoroutines();
        //Update the damage the weapon collider will use based on attack data
        _weaponCollider.UpdateAttackDamage(_currentAttackData.BaseDamage);
        //Setup and play animations associated with the attack data
        _attackAnimator.runtimeAnimatorController = _currentAttackData.OverrideController;
        _attackAnimator.Play("Attack");
        //Use SFX manager to setup attack sound
        SoundEffectManager.Instance.Builder
            .setSound(_currentAttackData.AttackSound)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
        //Begin coroutine that will measure the animation length and then reset weapon
        StartCoroutine(AttackLengthCoroutine(_currentAttackData.AnimationClip.length));
    }

    protected override void OnAwake()
    {
        _comboTree = new();
        _comboTree.InitTreeAttackData(_attackData);
    }

    protected override void OnPrimaryAction()
    {
        _currentAttackData = _comboTree.ProcessPrimaryAction().AttackData;
    }

    protected override void OnSecondaryAction()
    {
        _currentAttackData = _comboTree.ProcessSecondaryAction().AttackData;
    }

    protected override void OnAttackEnded()
    {
        StartCoroutine(_comboTree.ComboResetTimer(2f));
    }
}