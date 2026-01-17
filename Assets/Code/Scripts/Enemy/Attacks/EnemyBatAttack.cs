using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBatAttack : EnemyBaseAttack
{
    public override void Use(float damage)
    {
        isAttacking = true;
        attackDirection = (PlayerRef.PlayerTransform.position - transform.position).normalized;
        attackDirection.y = 0;
        _enemyManager.Rigidbody.isKinematic = false;
        _enemyManager.Rigidbody.AddForce(dashForce * attackDirection, ForceMode.Impulse);
        _enemyManager.Animator.Play("Attack");
        _enemyManager.StartCoroutine(AttackDuration());
    }

    private void Attack()
    {
        if (Physics.BoxCast(_enemyManager.transform.position, //Where casting ray
                   new Vector3(0.25f, 0.25f, 0.25f),          // size of half of box side lengths
                   attackDirection,                         //Direction of the ray
                   _enemyManager.transform.rotation,          //Current enemy rotation
                   1f,                                        //Distance ray is cast out
                   playerMask))                               //Player's layer mask
        {
            InvokeAttackEvent(damageAmount);
            isAttacking = false;
        }
    }
    private IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        _enemyManager.StartCoroutine(AttackCooldown());
    }
    private IEnumerator AttackCooldown()
    {
        _enemyManager.Rigidbody.isKinematic = true;
        yield return new WaitForSeconds(attackCooldown);
        //_enemyManager.SwitchState(EnemyStateCache.EnemyStates.BAT_CHASE);
    }

    private void Start()
    {
        playerMask = LayerMask.GetMask("Player");

    }
    private void Update()
    {
        if (!isAttacking) return;
        Attack();
    }

    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float attackDuration = 0.2f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float dashForce = 5f;
    private bool isAttacking = true;
    private LayerMask playerMask;
    private Vector3 attackDirection;
}
