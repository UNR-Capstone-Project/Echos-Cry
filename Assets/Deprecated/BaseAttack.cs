using System.Collections;
using UnityEngine;
using static ComboTree;

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
        switch (TempoConductor.Instance.CurrentHitQuality)
        {
            case TempoConductor.HitQuality.Excellent:
                damageMultiplier = 1.5f;
                break;
            case TempoConductor.HitQuality.Good:
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