using UnityEngine;

public class PlayerDamagable : MonoBehaviour, IDamageable
{
    [SerializeField] PlayerStats stats;
    public void Execute(float amount)
    {
        stats.Damage(amount);
    }
}
