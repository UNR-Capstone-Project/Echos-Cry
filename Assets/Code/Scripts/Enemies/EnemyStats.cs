using UnityEngine;

public class EnemyStats 
{
    private float _health;
    private float _maxHealth;
    public EnemyStats(float health, float maxHealth)
    {
        _health = health;
        _maxHealth = maxHealth;
    }
    public void UpdateHealth(float numChange)
    {
        _health = Mathf.Clamp(_health += numChange, 0, _maxHealth);
    }
}
