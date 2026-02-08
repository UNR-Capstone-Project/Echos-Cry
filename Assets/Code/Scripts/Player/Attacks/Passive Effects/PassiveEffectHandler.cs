using System;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEffectHandler : MonoBehaviour
{
    private HashSet<Type> _passiveEffectSet = new();
    
    public void UsePassiveEffect(PassiveEffect effect)
    {
        Type effectType = effect.GetType();

        if (!_passiveEffectSet.Add(effectType)) return; //Avoid duplicate effects.

        PassiveEffect effectInstance = Instantiate(effect);
        effectInstance.ApplyEffect(gameObject);
    }

    public void RemovePassiveEffect(PassiveEffect effect)
    {
        Type effectType = effect.GetType();
        _passiveEffectSet.Remove(effectType);
    }
}
