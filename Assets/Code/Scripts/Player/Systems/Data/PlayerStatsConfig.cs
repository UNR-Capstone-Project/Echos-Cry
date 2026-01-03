using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Player Data/Player Stats Config")]
public class PlayerStatsConfig : ScriptableObject
{
    [SerializeField] private FloatVariable _currentHealth;
    [SerializeField] private FloatVariable _maxHealth;
    public FloatVariable CurrentHealth { get { return _currentHealth; } }
    public FloatVariable MaxHealth { get { return _maxHealth; } }
}
