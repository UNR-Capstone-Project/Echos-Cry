using System.Collections;
using UnityEngine;

public abstract class PassiveEffect : ScriptableObject
{
    protected Enemy enemyReference;

    public bool isEffectOneTime = false;
    public float effectUseInterval;
    public float effectDuration;
    //public ComboStateMachine.StateName requiredState = ComboStateMachine.StateName.START;

    private bool isActive = false;

    public virtual void ApplyEffect(GameObject enemyObject)
    {
        enemyReference = enemyObject.GetComponent<Enemy>();

        isActive = true;
        enemyReference.StartCoroutine(EndRoutineEffect()); //Scriptable objects can't use coroutines, so starts from the enemy manager.

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
        enemyReference.PassiveEffectHandler.RemovePassiveEffect(this);
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