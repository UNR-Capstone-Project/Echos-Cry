using AudioSystem;
using System.Collections;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public void Move(Vector2 playerInputLocomotion)
    {
        if (_playerMovementConfig == null) return;

        Vector3 targetVel = (playerInputLocomotion.y * _playerMovementConfig.PlayerSpeed * forwardVector)
                          + (playerInputLocomotion.x * _playerMovementConfig.PlayerSpeed * rightVector)
                          + new Vector3(0f,_playerRigidbody.linearVelocity.y,0f);

        _playerRigidbody.AddForce(targetVel - _playerRigidbody.linearVelocity, ForceMode.VelocityChange);
    }
    public void Dash()
    {
        // Developer options for playtesting -- Please don't remove, just disable if you don't like them! Ask your friends too.
        if (_playerMovementConfig.IsDashToBeat && !TempoConductor.Instance.IsOnBeat()) return;

        _playerRigidbody.AddForce(_playerRigidbody.linearVelocity.normalized * _playerMovementConfig.DashSpeed, ForceMode.VelocityChange);
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

            rightVector = Camera.main.transform.right.normalized;
            rightVector.y = 0f;
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