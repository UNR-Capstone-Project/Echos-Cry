using System;
using System.Collections;
using UnityEngine;

public abstract class AttackStrategy : MonoBehaviour, IStrategy
{
    public virtual void Execute()
    {

    }
}

public class MeleeAttackStrategy : AttackStrategy
{
    private readonly Enemy _enemy;
    private readonly LayerMask _playerMask;
    private readonly float _attackDistance;
    private readonly Vector3 _boxExtents;
    private readonly float _attackCooldown;
    
    private bool _hasAttacked;
    
    public MeleeAttackStrategy(Enemy enemy, float attackDistance, Vector3 boxExtents, LayerMask playerMask, float attackCooldown)
    {
        _enemy = enemy;
        _playerMask = playerMask;
        _attackDistance = attackDistance;
        _boxExtents = boxExtents;
        _attackCooldown = attackCooldown;
    }

    public void Execute(float damage)
    {
        if (Physics.BoxCast(
                   _enemy.transform.position,
                   _boxExtents,
                   _enemy.transform.forward,
                   out RaycastHit hitInfo,
                   _enemy.transform.rotation,
                   _attackDistance,                        
                   _playerMask) 
            &&  !_hasAttacked)       
        {
            hitInfo.collider.gameObject.GetComponent<IDamageable>().Execute(damage);
            _hasAttacked = true;
            _enemy.StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _hasAttacked = false;
    }
}
