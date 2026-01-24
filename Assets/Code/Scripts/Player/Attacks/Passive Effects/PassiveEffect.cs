using System.Collections;
using UnityEngine;

public abstract class PassiveEffect : ScriptableObject
{
    protected EnemyStats enemyReference;
    public bool isEffectConstant = false;
    public bool isEffectRecurrent = false;
    public float effectUseInterval = 1f;
    public float effectDuration = 5f;
    private bool isActive = false;

    public virtual void ApplyEffect(EnemyStats parentEnemyRef)
    {
        enemyReference = parentEnemyRef;

        if (isEffectConstant)
        {
            UseEffect();
        }
        else
        {
            isActive = true;
            enemyReference.StartCoroutine(RoutineEffect());
            enemyReference.StartCoroutine(EndRoutineEffect());
        }
    }

    public virtual void RemoveEffect()
    {
        isActive = false;
        enemyReference.RemovePassiveEffect(this);
    }

    private IEnumerator EndRoutineEffect()
    {
        yield return new WaitForSeconds(effectDuration);
        RemoveEffect();
    }

    private IEnumerator RoutineEffect()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(effectUseInterval);
            UseEffect();
        }
    }

    public abstract void UseEffect();
}