using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Player Data/Player Stats Config")]
public class StatsConfig : ScriptableObject
{
    //[SerializeField] private FloatVariable _currentHealth;
    //[SerializeField] private FloatVariable _maxHealth;
    //public FloatVariable CurrentHealth { get { return _currentHealth; } }
    //public FloatVariable MaxHealth { get { return _maxHealth; } }
    public bool invincible;
    public float currentHealth;
    public float maxHealth;
    public float maxArmor;
    public float currentArmor;
}
