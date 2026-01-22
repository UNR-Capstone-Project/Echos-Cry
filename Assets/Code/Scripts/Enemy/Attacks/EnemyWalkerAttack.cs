using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWalkerAttack : EnemyBaseAttack
{
    public override void UseAttack()
    {
        attackDirection = (PlayerRef.PlayerTransform.position - transform.position).normalized;
        attackDirection.y = 0;
        _enemyManager.EnemyRigidbody.isKinematic = false;
        _enemyManager.EnemyRigidbody.AddForce(dashForce * attackDirection, ForceMode.Impulse);
        _enemyManager.EnemyAnimator.Play("Attack");
        _enemyManager.StartCoroutine(AttackDuration());
    }

    private IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(attackDuration);

        Instantiate(WalkerAttack, transform.position, Quaternion.identity);

        _enemyManager.StartCoroutine(AttackCooldown());
    }
    private IEnumerator AttackCooldown()
    {
        _enemyManager.EnemyRigidbody.isKinematic = true;
        yield return new WaitForSeconds(attackCooldown);
        _enemyManager.SwitchState(SimpleEnemyStateCache.EnemyStates.WALKER_CHASE);
    }

    [SerializeField] private float attackDuration = .65f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float dashForce = 7f;
    [SerializeField] private GameObject WalkerAttack;
    private Vector3 attackDirection;
}
