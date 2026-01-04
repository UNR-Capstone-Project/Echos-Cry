using AudioSystem;
using System.Collections;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public void PlayerMove(Vector2 playerInputLocomotion)
    {
        Vector3 forwardVector = mainCameraRef.forward.normalized;
        forwardVector.y = 0f;

        Vector3 rightVector = mainCameraRef.right.normalized;
        rightVector.y = 0f;

        Vector3 targetVel = (playerInputLocomotion.y * _playerMovementConfig.PlayerSpeed * forwardVector)
                          + (playerInputLocomotion.x * _playerMovementConfig.PlayerSpeed * rightVector)
                          + new Vector3(0f,_playerRigidbody.linearVelocity.y,0f);

        if (playerInputLocomotion != Vector2.zero) _playerRigidbody.AddForce(targetVel - _playerRigidbody.linearVelocity, ForceMode.VelocityChange);
        else _playerRigidbody.AddForce(-(stoppingAcceleration * _playerRigidbody.linearVelocity.normalized));
    }
    public void PlayerDash()
    {
        _playerRigidbody.AddForce(_playerRigidbody.linearVelocity.normalized * _playerMovementConfig.DashSpeed, ForceMode.Impulse);
    }

    void Start()
    {
        mainCameraRef = Camera.main.transform;

        stoppingAcceleration = _playerMovementConfig.PlayerSpeed * 2;
    }

    [Header("Configuration Object")]
    [SerializeField] private PlayerMovementConfig _playerMovementConfig;
    
    [Header("Player Movement System Dependencies")]
    [SerializeField] private Rigidbody _playerRigidbody;

    private Transform mainCameraRef;
    private float stoppingAcceleration;
}