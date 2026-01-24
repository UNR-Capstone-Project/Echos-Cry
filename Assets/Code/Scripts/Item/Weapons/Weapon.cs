using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Animator _attackAnimator;
    [SerializeField] protected WeaponCollider _weaponCollider;

    protected RuntimeAnimatorController _defaultAnimatorController;
    protected AttackData _currentAttackData;
    
    public bool IsAttackEnded { get; private set; }

    protected virtual void OnAwake() { } 
    protected virtual void OnPrimaryAction() { }
    protected virtual void OnSecondaryAction() { }
    protected virtual void OnAttackEnded() { }

    protected abstract void Attack();

    private void Awake()
    {
        _defaultAnimatorController = _attackAnimator.runtimeAnimatorController;
        SetChildrenActive(false);
        OnAwake();
    }
    private void AttackEnded()
    {
        _attackAnimator.runtimeAnimatorController = _defaultAnimatorController;
        IsAttackEnded = true;
        OnAttackEnded();
        SetChildrenActive(false);
    }
    public void PrimaryAction()
    {
        SetChildrenActive(true);
        IsAttackEnded = false;
        OnPrimaryAction();
        Attack();
    }
    public void SecondaryAction() 
    {
        SetChildrenActive(true);
        IsAttackEnded = false;
        OnSecondaryAction();
        Attack();
    }

    private void SetChildrenActive(bool active)
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(active);
        }
    }
    protected IEnumerator AttackLengthCoroutine(float animationLength)
    {
        yield return new WaitForSeconds(animationLength);
        AttackEnded();
    }
}