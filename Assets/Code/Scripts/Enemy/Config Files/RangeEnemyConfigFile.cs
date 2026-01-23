using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Enemy/Enemy Config File/Range")]
public class RangeEnemyConfigFile : ScriptableObject
{
    [SerializeField] private float _distanceCheck;
    [SerializeField] private float _attackChargeTime;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _staggerDuration;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private int _attackAmount;
    public float DistanceCheck { get { return _distanceCheck; } }
    public float AttackChargeTime { get { return _attackChargeTime; } }
    public float AttackCooldown { get => _attackCooldown; }
    public float StaggerDuration { get => _staggerDuration; }
    public float KnockbackForce { get => _knockbackForce; }
    public int AttackAmount { get => _attackAmount; }
}
