using System;
using UnityEngine;
using static ComboStateMachine;

[RequireComponent(typeof(Animator))]
public abstract class BaseWeapon : MonoBehaviour
{
    //Must only be of the same length - 1 of the StateName enum
    [SerializeField] protected AttackData[] _attackData;
    protected Animator _attackAnimator;
    protected RuntimeAnimatorController _defaultAnimatorController;
    public static event Action OnAttackEndedEvent;
    public static event Action<float> UpdateColliderAttackDamageEvent;

    protected abstract void Attack(StateName attackState);

    protected void OnAttackEnded()
    {
        OnAttackEndedEvent?.Invoke();
    }
    protected void UpdateColliderAttackDamage(float newDamage)
    {
        UpdateColliderAttackDamageEvent?.Invoke(newDamage);
    }

    protected virtual void Awake()
    {
        _attackAnimator = GetComponent<Animator>();
        _defaultAnimatorController = _attackAnimator.runtimeAnimatorController;
    }
}
