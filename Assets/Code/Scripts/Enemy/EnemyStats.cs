using AudioSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Handles enemy stats such as health, armor etc
//Contains relevant functions and events for each stat/event

public class EnemyStats : MonoBehaviour
{
    public HashSet<Type> passiveEffectSet; //Set used to avoid duplicate effects.
    private float damageMultiplier = 1f;

    public float Health { get; private set; }
    public float MaxHealth = 100f;

    public event Action<float, Color> OnEnemyDamagedEvent;
    public event Action OnEnemyHealedEvent;
    public event Action OnEnemyDeathEvent;

    private void Awake()
    {
        passiveEffectSet = new HashSet<Type>();
    }
    private void Start()
    {
        Health = MaxHealth;
    }

    //-----Passive Effects Management-----//
    public void UsePassiveEffect(PassiveEffect effect)
    {
        Type effectType = effect.GetType();

        if (!passiveEffectSet.Add(effectType)) return; //Avoid duplicate effects.

        PassiveEffect instance = Instantiate(effect);
        instance.ApplyEffect(this);
    }
    public void RemovePassiveEffect(PassiveEffect effect)
    {
        Type effectType = effect.GetType();
        passiveEffectSet.Remove(effectType);
    }

    public void SetDamageMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }

    //-----Health Management-----//
    public void HealEnemy(float heal)
    {
        Health += Mathf.Abs(heal);
        if (Health > MaxHealth) Health = MaxHealth;
        OnEnemyHealedEvent?.Invoke();
    }
    public void DamageEnemy(float damage, Color color)
    {
        damage = damage * damageMultiplier;
        Health -= Mathf.Abs(damage);
        OnEnemyDamagedEvent?.Invoke(damage, color);
    }
    public void HandleEnemyDeath()
    {
        OnEnemyDeathEvent?.Invoke();
        Destroy(gameObject);
    }
}
