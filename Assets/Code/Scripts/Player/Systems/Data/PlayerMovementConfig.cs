using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Player Data/Player Movement Config")]
public class PlayerMovementConfig : ScriptableObject
{
    [Header("Player Movement Variables")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _attackMomentumSpeed;
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private int _dashCount;
    [SerializeField] private float _attackMomentumSpeed;
    
    [Header("Dev Testing variable")]
    [SerializeField] private bool _isDashToBeat = true;
    [SerializeField] private bool _hasDashCooldown = true;
    
    public float DashSpeed { get { return _dashSpeed; } }
    public float DashDuration { get { return _dashDuration; } }
    public float AttackMomentumSpeed { get { return _attackMomentumSpeed; } }
    public float DashCooldown { get { return _dashCooldown; } }
    public float PlayerSpeed { get { return _playerSpeed; } }
    public int DashCount { get => _dashCount; }
    public bool IsDashToBeat { get => _isDashToBeat; }
    public bool HasDashCooldown { get => _hasDashCooldown; }
    public float AttackMomentumSpeed { get => _attackMomentumSpeed; }
}
