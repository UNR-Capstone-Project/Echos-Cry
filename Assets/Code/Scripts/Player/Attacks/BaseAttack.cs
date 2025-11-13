using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static Attack;

public class BaseAttack : MonoBehaviour
{
    public float attackDamage = 1f;
    public float heavyAttackModifier = 1.5f;
    public float destroyTime = 1f;
    public float attackWait = 1f;
    public float knockForce = 1f;

    private float damageMultiplier = 1f;
    protected float totalAttackDamage => attackDamage * damageMultiplier;
    protected Attack.AttackType attackType;

    public float GetAttackWait()
    {
        return attackWait;
    }

    public float GetAttackDamage()
    {
        return totalAttackDamage;
    }

    public void SetDamageMultiplier(float multiplier)
    {
        if (attackType == Attack.AttackType.LIGHT)
        {
            damageMultiplier = multiplier;
        }
        else if (attackType == Attack.AttackType.HEAVY)
        {
            damageMultiplier = multiplier * heavyAttackModifier;
        }
    }

    public void StartAttack(float damageMultiplier, Attack.AttackType type)
    {
        attackType = type;
        SetDamageMultiplier(damageMultiplier);
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