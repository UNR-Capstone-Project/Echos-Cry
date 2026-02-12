using AudioSystem;
using System.Collections;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] BoolEventChannel _lockMovementChannel;
    private bool _isMovementLocked = false;

    public void Move(Vector2 playerInputLocomotion)
    {
        if (_isMovementLocked) return;
        if (_playerMovementConfig == null) return;

        Vector3 targetVel = (playerInputLocomotion.y * _moveSpeed * forwardVector)
                          + (playerInputLocomotion.x * _moveSpeed * rightVector)
                          + new Vector3(0f, _playerRigidbody.linearVelocity.y, 0f);

        _playerRigidbody.AddForce(targetVel - _playerRigidbody.linearVelocity, ForceMode.VelocityChange);
    }
    public void Dash()
    {
        if (_isMovementLocked) return;
        if (_playerMovementConfig == null) return;
        _playerRigidbody.AddForce(_playerRigidbody.linearVelocity.normalized * _dashSpeed, ForceMode.VelocityChange);
        _dashCount--;
        StartDashCooldown();
    }

    private IEnumerator AddDashCoroutine()
    {
        yield return new WaitForSeconds(_dashCooldown);
        _dashCount++;
    }
    public void StartDashCooldown()
    {
        StartCoroutine(AddDashCoroutine());
    }

    public void MomentumPush(Vector3 direction)
    {
        if (_isMovementLocked) return;
        if (_playerMovementConfig == null) return;
        //Working on making momentum maintain when attacking in opposite direction
        float directionDot = Vector3.Dot(direction.normalized, _playerRigidbody.linearVelocity.normalized);
        _playerRigidbody.AddForce(_playerMovementConfig.AttackMomentumSpeed * directionDot * direction, ForceMode.VelocityChange);
    }

    private void OnEnable()
    {
        _lockMovementChannel.Channel += (state) => _isMovementLocked = state;
        _dashCount = _playerMovementConfig.DashCount;
    }
    private void OnDisable()
    {
        _lockMovementChannel.Channel -= (state) => _isMovementLocked = state;
        StopAllCoroutines();
    }

    void Start()
    {
        if(_playerMovementConfig == null)
        {
            Debug.LogWarning("Player Movement Configuration file is null!");
            return;
        }
        else
        {
            _dashSpeed = _playerMovementConfig.DashSpeed;
            _moveSpeed = _playerMovementConfig.PlayerSpeed;
            _dashCount = _playerMovementConfig.DashCount;
            _dashCooldown = _playerMovementConfig.DashCooldown;
        }

        if (Camera.main != null)
        {
            forwardVector = Camera.main.transform.forward.normalized;
            forwardVector.y = 0f;

            rightVector = Camera.main.transform.right.normalized;
            rightVector.y = 0f;
        }
    }

    [Header("Configuration Object")]
    [SerializeField] private PlayerMovementConfig _playerMovementConfig;
    public PlayerMovementConfig PlayerMovementConfig { get { return _playerMovementConfig; } }
    
    [Header("Player Movement System Dependencies")]
    [SerializeField] private Rigidbody _playerRigidbody;

    private float _dashSpeed;
    private float _moveSpeed;
    private float _dashCooldown;
    private int _dashCount;
    public float DashSpeed { get => _dashSpeed; set => _dashSpeed = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float DashCooldown { get => _dashCooldown; set => _dashCooldown = value; }
    public int DashCount { get => _dashCount; set => _dashCount = value; }

    public bool HasDash => _dashCount > 0;

    private Vector3 forwardVector;
    private Vector3 rightVector;
}