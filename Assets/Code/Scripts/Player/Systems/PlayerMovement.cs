using AudioSystem;
using System.Collections;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] BoolEventChannel _lockMovementChannel;
    private bool _isMovementLocked = false;
    private Vector3 _lastMoveDirection;

    private void OnEnable()
    {
        _lockMovementChannel.Channel += (state) => _isMovementLocked = state;
    }
    private void OnDisable()
    {
        _lockMovementChannel.Channel -= (state) => _isMovementLocked = state;
    }

    public void Move(Vector2 playerInputLocomotion)
    {
        if (_isMovementLocked) return;
        if (_playerMovementConfig == null) return;

        Vector3 movementDirection = (playerInputLocomotion.y * forwardVector
                                   + playerInputLocomotion.x * rightVector);

        Vector3 targetVel = movementDirection * _playerMovementConfig.PlayerSpeed
                          + new Vector3(0f, _playerRigidbody.linearVelocity.y, 0f);

        if (movementDirection.sqrMagnitude > 0.01f)
            _lastMoveDirection = movementDirection.normalized;

        _playerRigidbody.AddForce(targetVel - _playerRigidbody.linearVelocity, ForceMode.VelocityChange);
    }

    public void Dash()
    {
        if (_isMovementLocked) return;
        if (_playerMovementConfig == null) return;
        if (_lastMoveDirection == Vector3.zero) return;

        _playerRigidbody.AddForce(_lastMoveDirection * _playerMovementConfig.DashSpeed, ForceMode.VelocityChange);
    }

    public void StopHorizontalMovement()
    { //When player goes from dash -> idle, the movement is never stopped causing a slide, this ensures motion is stopped.
        _playerRigidbody.linearVelocity = new Vector3(0f, _playerRigidbody.linearVelocity.y, 0f);
    }

    void Start()
    {
        if(_playerMovementConfig == null)
        {
            Debug.LogWarning("Player Movement Configuration file is null!");
            return;
        }

        if (Camera.main != null)
        {
            forwardVector = Camera.main.transform.forward.normalized;
            forwardVector.y = 0f;
            forwardVector.Normalize();

            rightVector = Camera.main.transform.right.normalized;
            rightVector.y = 0f;
            rightVector.Normalize();
        }
    }

    [Header("Configuration Object")]
    [SerializeField] private PlayerMovementConfig _playerMovementConfig;
    public PlayerMovementConfig PlayerMovementConfig { get { return _playerMovementConfig; } }
    
    [Header("Player Movement System Dependencies")]
    [SerializeField] private Rigidbody _playerRigidbody;

    private Vector3 forwardVector;
    private Vector3 rightVector;
}