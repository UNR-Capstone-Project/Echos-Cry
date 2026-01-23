using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Enemy/Enemy Config File/Walker")]
public class WalkerEnemyConfigFile : ScriptableObject
{
    [SerializeField] private float _distanceCheck;
    [SerializeField] private float _attackChargeTime;
    [SerializeField] private float _staggerDuration;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _attackDuration;
    [SerializeField] private float _attackDashForce;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _stoppingDistance;

    public float DistanceCheck { get => _distanceCheck; }
    public float AttackChargeTime { get => _attackChargeTime; }
    public float StaggerDuration { get => _staggerDuration; }
    public float KnockbackForce { get => _knockbackForce; }
    public float AttackDuration { get => _attackDuration; }
    public float AttackDashForce { get => _attackDashForce; }
    public float AttackCooldown { get => _attackCooldown; }
    public float StoppingDistance { get => _stoppingDistance; }
}
