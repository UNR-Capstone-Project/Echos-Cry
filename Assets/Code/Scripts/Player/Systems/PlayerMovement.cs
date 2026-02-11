using AudioSystem;
using System.Collections;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public void Move(Vector2 playerInputLocomotion)
    {
        if (_playerMovementConfig == null) return;

        Vector3 targetVel = (playerInputLocomotion.y * _moveSpeed * forwardVector)
                          + (playerInputLocomotion.x * _moveSpeed * rightVector)
                          + new Vector3(0f, _playerRigidbody.linearVelocity.y, 0f);

        _playerRigidbody.AddForce(targetVel - _playerRigidbody.linearVelocity, ForceMode.VelocityChange);
    }
    public void Dash()
    {
        if (_playerMovementConfig == null) return;
        _playerRigidbody.AddForce(_playerRigidbody.linearVelocity.normalized * _dashSpeed, ForceMode.VelocityChange);
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
    public float DashSpeed { get => _dashSpeed; set => _dashSpeed = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    private Vector3 forwardVector;
    private Vector3 rightVector;
}