using AudioSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//Handles enemy stats such as health, armor etc
//Contains relevant functions and events for each stat/event

public class EnemyStats : MonoBehaviour
{
    public float Health { get; private set; }
    public float MaxHealth = 100f;
    public float Armor {  get; private set; }
    public float MaxArmor;

    public event Action<float, Color> OnEnemyDamagedEvent;
    public event Action OnEnemyHealedEvent;
    public event Action OnEnemyDeathEvent;

    private void Start()
    {
        Health = MaxHealth;
    }
 
    public void HealHealth(float heal)
    {
        Health += Mathf.Abs(heal);
        if (Health > MaxHealth) Health = MaxHealth;
        OnEnemyHealedEvent?.Invoke();
    }
    public void HealArmor(float heal)
    {
        Armor += Mathf.Abs(heal);
        if (Health > MaxHealth) Health = MaxHealth;
        OnEnemyHealedEvent?.Invoke();
    }
    public void DamageHealth(float damage, Color color)
    {
        Health -= Mathf.Abs(damage);
        OnEnemyDamagedEvent?.Invoke(damage, color);
    }
    public void DamageArmor(float damage, Color color)
    {
        Armor -= Mathf.Abs(damage);
        OnEnemyDamagedEvent?.Invoke(damage, color);
    }
    public void HandleEnemyDeath()
    {
        OnEnemyDeathEvent?.Invoke();
        Destroy(gameObject);
    }
}
