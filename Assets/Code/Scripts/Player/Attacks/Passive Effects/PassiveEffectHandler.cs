using System;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEffectHandler : MonoBehaviour
{
    [SerializeField] private Enemy _enemyReference;
    public HashSet<Type> passiveEffects = new();

    public void UsePassiveEffect(PassiveEffect effect)
    {
        Type effectType = effect.GetType();

        if (!passiveEffects.Add(effectType)) return; //Avoid duplicate effects.

        PassiveEffect effectInstance = Instantiate(effect);
        effectInstance.ApplyEffect(_enemyReference);
    }

    public void RemovePassiveEffect(PassiveEffect effect)
    {
        Type effectType = effect.GetType();
        passiveEffects.Remove(effectType);
    }
}
