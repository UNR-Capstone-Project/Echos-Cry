using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Animator _attackAnimator;
    protected RuntimeAnimatorController _defaultAnimatorController;
    
    public static bool IsAttackEnded { get; private set; }

    private void Awake()
    {
        _defaultAnimatorController = _attackAnimator.runtimeAnimatorController;
        OnAwake();
    }

    protected virtual void OnAwake() { } 
    protected virtual void OnPrimaryAction() { }
    protected virtual void OnSecondaryAction() { }
    protected IEnumerator AttackLengthCoroutine(float animationLength)
    {
        yield return new WaitForSeconds(animationLength);
        _attackAnimator.runtimeAnimatorController = _defaultAnimatorController;
        IsAttackEnded = true;
    }

    public void PrimaryAction()
    {
        IsAttackEnded = false;
        OnPrimaryAction();
    }
    public void SecondaryAction() 
    {
        IsAttackEnded = false;
        OnSecondaryAction();
    }
}

public class ComboWeapon : Weapon
{
    private ComboTree _comboSystem;
    [SerializeField] private AttackData[] _attackData;

    protected override void OnAwake()
    {
        _comboSystem = new();
        _comboSystem.InitTreeAttackData(_attackData);
    }

    protected override void OnPrimaryAction()
    {
    
    }

    protected override void OnSecondaryAction()
    {
    
    }
}