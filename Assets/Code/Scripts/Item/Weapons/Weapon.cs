using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Weapon : MonoBehaviour
{
    //Must only be of the same length - 1 of the StateName enum
    [SerializeField] protected AttackData[] _attackData;
    protected Animator _attackAnimator;
    protected RuntimeAnimatorController _defaultAnimatorController;

    public static event Action<ComboStateMachine.StateName> OnAttackStartEvent;
    public static event Action OnAttackEndedEvent;

    public static event Action<float> UpdateColliderAttackDamageEvent;
    public abstract void PrimaryAction();

    public abstract void SecondaryAction();

    protected virtual void Attack(ComboStateMachine.StateName attackState)
    {
        IsAttackEnded = false;
        OnAttackStartEvent?.Invoke(attackState);
    }

    protected void OnAttackEnded()
    {
        IsAttackEnded = true;
        OnAttackEndedEvent?.Invoke();
    }
    protected void UpdateColliderAttackDamage(float newDamage)
    {
        UpdateColliderAttackDamageEvent?.Invoke(newDamage);
    }

    public static bool IsAttackEnded { get; private set; }

    protected virtual void Awake()
    {
        _attackAnimator = GetComponent<Animator>();
        _defaultAnimatorController = _attackAnimator.runtimeAnimatorController;
    }
}
