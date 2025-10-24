using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private float _health;
    public float MaxHealth = 100f;
    public event Action OnEnemyDamagedEvent;
    public event Action OnEnemyHealedEvent;

    private void Start()
    {
        _health = MaxHealth;
    }

    public void UpdateHealth(float numChange)
    {
        _health = Mathf.Clamp(_health += numChange, 0, MaxHealth);

        if (numChange > 0) OnEnemyHealedEvent?.Invoke();
        else OnEnemyDamagedEvent?.Invoke();
    }
}
