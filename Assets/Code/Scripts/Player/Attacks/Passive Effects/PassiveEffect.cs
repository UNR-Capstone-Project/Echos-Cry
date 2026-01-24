using System.Collections;
using UnityEngine;

public abstract class PassiveEffect : ScriptableObject
{
    protected EnemyStats enemyReference;

    public bool isEffectOneTime = false;
    public float effectUseInterval = 1f;
    public float effectDuration = 5f;
    public ComboStateMachine.StateName requiredState = ComboStateMachine.StateName.START;

    private bool isActive = false;

    public virtual void ApplyEffect(EnemyStats parentEnemyRef)
    {
        enemyReference = parentEnemyRef;

        isActive = true;
        enemyReference.StartCoroutine(EndRoutineEffect());

        if (isEffectOneTime)
        {
            UseEffect();
        }
        else
        {
            enemyReference.StartCoroutine(RoutineEffect());
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