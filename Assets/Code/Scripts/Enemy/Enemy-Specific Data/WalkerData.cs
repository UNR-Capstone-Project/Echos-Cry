using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Enemy/Enemy Data/Walker")]
public class WalkerData : ScriptableObject
{
    [SerializeField] private float _distanceCheck;
    [SerializeField] private float _attackChargeTime;
    [SerializeField] private float _staggerDuration;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpDashForce;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _stoppingDistance;
    [SerializeField] private GameObject _fireRingPrefab;
    [SerializeField] private float _fireRingTime;

    public float DistanceCheck { get => _distanceCheck; }
    public float AttackChargeTime { get => _attackChargeTime; }
    public float StaggerDuration { get => _staggerDuration; }
    public float KnockbackForce { get => _knockbackForce; }
    public float JumpDuration { get => _jumpDuration; }
    public float JumpDashForce { get => _jumpDashForce; }
    public float AttackCooldown { get => _attackCooldown; }
    public float StoppingDistance { get => _stoppingDistance; }
    public GameObject FireRingPrefab { get => _fireRingPrefab; }
    public float FireRingTime { get => _fireRingTime; }
}
