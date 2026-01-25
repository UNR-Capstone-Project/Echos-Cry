using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Enemy/Enemy Data/Range")]
public class RangeData : ScriptableObject
{
    [SerializeField] private float _distanceCheck;
    [SerializeField] private float _attackChargeTime;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _staggerDuration;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private int _projectileCount;
    [SerializeField] private float _distanceFromPlayer;

    public readonly int WalkHashCode = Animator.StringToHash("Walk");
    public float DistanceCheck { get { return _distanceCheck; } }
    public float AttackChargeTime { get { return _attackChargeTime; } }
    public float AttackCooldown { get => _attackCooldown; }
    public float StaggerDuration { get => _staggerDuration; }
    public float KnockbackForce { get => _knockbackForce; }
    public int ProjectileCount { get => _projectileCount; }
    public float DistanceFromPlayer { get => _distanceFromPlayer; }
}
