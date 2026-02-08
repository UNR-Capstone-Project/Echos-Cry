using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEffectHandler : MonoBehaviour
{
    [SerializeField] private Enemy enemyReference;

    private HashSet<Type> _passiveEffectSet = new();
    private Dictionary<Type, Coroutine> _activeRoutines = new();
    
    public void UsePassiveEffect(PassiveEffect effect)
    {
        Type effectType = effect.GetType();

        if (!_passiveEffectSet.Add(effectType)) return; //Avoid duplicate effects. Can't use variants of the same effect at the same time, but can use different effects.

        ApplyEffect(effect);
    }

    public void RemovePassiveEffect(PassiveEffect effect)
    {
        Type type = effect.GetType();
        
        if (_activeRoutines.TryGetValue(type, out var routine))
        {
            StopCoroutine(routine);
            _activeRoutines.Remove(type);
        }

        _passiveEffectSet.Remove(type);
    }

    private void ApplyEffect(PassiveEffect effect)
    {
        StartCoroutine(EndRoutineEffect(effect)); //Scriptable objects can't use coroutines, so starts from the enemy manager.

        if (effect.isEffectOneTime)
        {
            UseEffect(effect);
        }
        else
        {
            Coroutine routine = StartCoroutine(RoutineEffect(effect));
            _activeRoutines[effect.GetType()] = routine;
        }
    }

    private IEnumerator EndRoutineEffect(PassiveEffect effect)
    {
        yield return new WaitForSeconds(effect.effectDuration);
        RemovePassiveEffect(effect);
    }

    private IEnumerator RoutineEffect(PassiveEffect effect)
    {
        while (_passiveEffectSet.Contains(effect.GetType()))
        {
            yield return new WaitForSeconds(effect.effectUseInterval);
            UseEffect(effect);
        }
    }

    public void UseEffect(PassiveEffect effect)
    {
        switch (effect)
        {
            case BleedPassiveEffect bleed:
                enemyReference.Health.Damage(bleed.bleedDamage, Color.red);

                enemyReference.SoundStrategy.Execute(enemyReference.SoundConfig.HitSFX, enemyReference.transform, 0);
                enemyReference.NPCAnimator.TintFlash(Color.red, 0.2f);
                enemyReference.NPCAnimator.PlayVisualEffect();

                if (DamageLabelManager.Instance != null)
                    DamageLabelManager.Instance.SpawnPopup(bleed.bleedDamage, enemyReference.transform.position, Color.red);
                break;
            case MarkedForDeathPassive marked:
                enemyReference.Health.SetDamageMultiplier(marked.damageMultiplier);
                break;
        }
    }
}