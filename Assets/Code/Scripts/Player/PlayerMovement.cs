using AudioSystem;
using System.Collections;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public void PlayerMove()
    {
        Vector3 forwardVector = _mainCameraTransform.forward.normalized;
        forwardVector.y = 0f;

        Vector3 rightVector = _mainCameraTransform.right.normalized;
        rightVector.y = 0f;

        Vector3 targetVel = (_playerInputLocomotion.y * _playerMovementConfig.PlayerSpeed * forwardVector)
                          + (_playerInputLocomotion.x * _playerMovementConfig.PlayerSpeed * rightVector)
                          + new Vector3(0f,_playerRigidbody.linearVelocity.y,0f);

        //if (_playerInputLocomotion != Vector2.zero) 
            _playerRigidbody.AddForce(targetVel - _playerRigidbody.linearVelocity, ForceMode.VelocityChange);
        //else _playerRigidbody.AddForce(-(_stoppingAcceleration * _playerRigidbody.linearVelocity.normalized));
    }
    public void PlayerDash()
    {
        _playerRigidbody.AddForce(_playerRigidbody.linearVelocity.normalized * _playerMovementConfig.DashSpeed, ForceMode.Impulse);
    }

    private void HandleInputLocomotion(Vector2 locomotion)
    {
        _playerInputLocomotion = locomotion;
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

        _inputTranslator.OnMovementEvent += HandleInputLocomotion;
    }
    private void OnDestroy()
    {
        _inputTranslator.OnMovementEvent -= HandleInputLocomotion;
    }

    [Header("Configuration Object")]
    [SerializeField] private PlayerMovementConfig _playerMovementConfig;
    
    [Header("Player Movement System Dependencies")]
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private InputTranslator _inputTranslator;

    private Transform _mainCameraTransform;
    private float _stoppingAcceleration;
    private Vector2 _playerInputLocomotion;
}