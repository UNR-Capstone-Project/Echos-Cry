using System.Collections;
using UnityEngine;
using static ComboStateMachine;

public class BaseAttack : MonoBehaviour
{
    public float destroyTime = 1f;
    public float attackWait = 1f;
    public float knockForce = 1f;

    private float damageMultiplier = 1f;
    private AttackData _currentAttack = null;
    public float TotalAttackDamage => _currentAttack.BaseDamage * damageMultiplier;

    public void SetDamageMultiplier()
    {
        switch (TempoManager.CurrentHitQuality)
        {
            case TempoManager.HIT_QUALITY.EXCELLENT:
                damageMultiplier = 1.5f;
                break;
            case TempoManager.HIT_QUALITY.GOOD:
                damageMultiplier = 1.25f;
                break;
            //case TempoManager.HIT_QUALITY.BAD:
            //    damageMultiplier = 1f;
            //    break;
            default:
                break;
        }
    }

    public void StartAttack(AttackData currentAttack)
    {
        _currentAttack = currentAttack;
        SetDamageMultiplier();
        InitAttack();
    }

    protected virtual void InitAttack()
    {
        StartDestroy(destroyTime);
    }

    protected void StartDestroy(float time)
    {
        StartCoroutine(TimedDestroy(time));
    }

    protected IEnumerator TimedDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}