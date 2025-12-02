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

    public event Action<float> OnEnemyDamagedEvent;
    public event Action OnEnemyHealedEvent;
    public event Action OnEnemyDeathEvent;

    private void Start()
    {
        Health = MaxHealth;
    }
 
    public void HealEnemy(float heal)
    {
        Health += Mathf.Abs(heal);
        if (Health > MaxHealth) Health = MaxHealth;
        OnEnemyHealedEvent?.Invoke();
    }
    public void DamageEnemy(float damage)
    {
        Health -= Mathf.Abs(damage);
        OnEnemyDamagedEvent?.Invoke(damage);
    }
    public void HandleEnemyDeath()
    {
        OnEnemyDeathEvent?.Invoke();
        Destroy(gameObject);
    }
}
