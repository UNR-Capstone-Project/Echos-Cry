using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Player Data/Player Movement Config")]
public class PlayerMovementConfig : ScriptableObject
{
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _playerSpeed;
    public float DashSpeed { get { return _dashSpeed; } }
    public float DashDuration { get { return _dashDuration; } }
    public float DashCooldown { get { return _dashCooldown; } }
    public float PlayerSpeed { get { return _playerSpeed; } }
}
