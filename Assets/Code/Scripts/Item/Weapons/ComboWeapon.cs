using UnityEngine;

public class ComboWeapon : Weapon
{
    private ComboTree _comboTree;
    [SerializeField] private ComboWeaponData _comboWeaponData;

    protected override void Attack()
    {
        //Stop Coroutines, specifically ComboResetTimer
        StopAllCoroutines();
        //Update the damage the weapon collider will use based on attack data
        _weaponCollider.UpdateAttackDamage(_currentAttackData.BaseDamage);
        //Setup and play animations associated with the attack data
        _attackAnimator.runtimeAnimatorController = _currentAttackData.OverrideController;
        _attackAnimator.Play("Attack");
        //Begin coroutine that will measure the animation length and then reset weapon

        StartCoroutine(AttackLengthCoroutine(_currentAttackData.AnimationClip.length));
    }

    protected override void OnAwake()
    {
        _comboTree = new();
        _comboTree.InitTreeAttackData(_comboWeaponData.AttackData);
    }

    protected override void OnPrimaryAction()
    {
        _currentAttackData = _comboTree.ProcessPrimaryAction().AttackData;
        
        //Use SFX manager to setup attack sound
        SoundEffectManager.Instance.Builder
            .SetSound(_currentAttackData.AttackSound)
            .SetSoundPosition(transform.position)
            .ValidateAndPlaySound();
    }

    protected override void OnSecondaryAction()
    {
        _currentAttackData = _comboTree.ProcessSecondaryAction().AttackData;

        //Use SFX manager to setup attack sound
        SoundEffectManager.Instance.Builder
            .SetSound(_currentAttackData.AttackSound)
            .SetSoundPosition(transform.position)
            .SetDelay(_comboWeaponData.SecondarySoundDelay)
            .ValidateAndPlaySound();
    }

    protected override void OnAttackEnded()
    {
        StartCoroutine(_comboTree.ComboResetTimer(_comboWeaponData.ComboResetTime));
    }
}