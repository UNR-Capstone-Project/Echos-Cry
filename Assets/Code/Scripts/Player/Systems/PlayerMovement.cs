using AudioSystem;
using System.Collections;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public void Move(Vector2 playerInputLocomotion)
    {
        if (_playerMovementConfig == null) return;

        Vector3 forwardVector = _mainCameraTransform.forward.normalized;
        forwardVector.y = 0f;

        Vector3 rightVector = _mainCameraTransform.right.normalized;
        rightVector.y = 0f;

        Vector3 targetVel = (playerInputLocomotion.y * _playerMovementConfig.PlayerSpeed * forwardVector)
                          + (playerInputLocomotion.x * _playerMovementConfig.PlayerSpeed * rightVector)
                          + new Vector3(0f,_playerRigidbody.linearVelocity.y,0f);

        _playerRigidbody.AddForce(targetVel - _playerRigidbody.linearVelocity, ForceMode.VelocityChange);
    }
    public void Dash()
    {
        _playerRigidbody.AddForce(_playerRigidbody.linearVelocity.normalized * _playerMovementConfig.DashSpeed, ForceMode.Impulse);
    }

    void Start()
    {
        _mainCameraTransform = Camera.main.transform;

        if(_playerMovementConfig == null)
        {
            Debug.LogWarning("Player Movement Configuration file is null!");
            return;
        }

        _stoppingAcceleration = _playerMovementConfig.PlayerSpeed * 2;

    }

    [Header("Configuration Object")]
    [SerializeField] private PlayerMovementConfig _playerMovementConfig;
    
    [Header("Player Movement System Dependencies")]
    [SerializeField] private Rigidbody _playerRigidbody;

    private Transform _mainCameraTransform;
    private float _stoppingAcceleration;
}