using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private float _health;
    public float MaxHealth = 100f;

    private void Start()
    {
        _health = MaxHealth;
    }

    public void UpdateHealth(float numChange)
    {
        _health = Mathf.Clamp(_health += numChange, 0, MaxHealth);
    }
}
