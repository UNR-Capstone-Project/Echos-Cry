using System;
using System.Collections;
using UnityEngine;

//This will be placed on every enemy to handle collision with an attack from player

//VERY VERY VERY IMPORTANT NOTE: this will only register collisions from colliders with the layer PlayerAttack placed on them
// so place PlayerAttack on the player's attack handler or whatever handles weapon collision

//This is used to increase combo meter/multiplier and set enemy to stagger state

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseEnemyCollisionHandler : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider collision)
    {
        if(collision.TryGetComponent<AttackCollisionHandler>(out AttackCollisionHandler handler))
        {
            PlayerComboMeter.AddToComboMeter(handler.AttackDamage);
            PlayerComboMeter.UpdateComboMultiplier();
        }
        //_enemyManager.SwitchState(SimpleEnemyStateCache.EnemyStates.BAT_STAGGER);
        _enemyCollider.enabled = false;
    }
    private void ResetColliderBool()
    {
        _enemyCollider.enabled = true;
    }

    private void Awake()
    {
        _enemyManager = GetComponent<SimpleEnemyManager>();
        _enemyCollider = GetComponent<Collider>();
    }
    private void Start()
    {
        BaseWeapon.OnAttackEndedEvent += ResetColliderBool;
    }
    private void OnDestroy()
    {
        BaseWeapon.OnAttackEndedEvent -= ResetColliderBool;
    }
    
    private Collider _enemyCollider;
    private SimpleEnemyManager _enemyManager;
}
