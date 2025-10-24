using System;
using UnityEngine;
using UnityEngine.AI;

//Handles enemy stats such as health, armor etc
//Contains relevant functions and events for each stat/event

[RequireComponent(typeof(EnemyInfo))]
[RequireComponent(typeof(HandleDamageCollision))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyStats : MonoBehaviour
{
    private float _health;
    public float MaxHealth = 100f;
    public event Action OnEnemyDamagedEvent;
    public event Action OnEnemyHealedEvent;

    public void HealEnemy(float heal)
    {
        _health += Mathf.Abs(heal);
        if (_health > MaxHealth) _health = MaxHealth;
        OnEnemyHealedEvent?.Invoke();
    }
    public void DamageEnemy(float damage)
    {
        _health -= Mathf.Abs(damage);
        if (_health < 0)
        {
            //do something
        }
        OnEnemyDamagedEvent?.Invoke();
        Debug.Log("Health: " + _health);
    }

    private void Start()
    {
        _health = MaxHealth;
    }
}
