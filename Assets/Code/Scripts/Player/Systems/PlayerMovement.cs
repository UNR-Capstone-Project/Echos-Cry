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
                          + new Vector3(0f, _playerRigidbody.linearVelocity.y, 0f);

        _playerRigidbody.AddForce(targetVel - _playerRigidbody.linearVelocity, ForceMode.VelocityChange);
    }
    public void Dash()
    {
        if (_playerMovementConfig == null) return;
        if (DashCounter > 0 && DashGenerationProgress == 1f)
        {
            DashCounter = 0;
            _playerRigidbody.AddForce(_playerRigidbody.linearVelocity.normalized * _playerMovementConfig.DashSpeed, ForceMode.VelocityChange);
            StartCoroutine(IncrementDashProgress());
        }
        
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

    private IEnumerator IncrementDashProgress()
    {
        //Debug.Log("Dash Reset Starting.");
        float elapsed = 0;
        while (elapsed < _playerMovementConfig.DashCooldown)
        {
            elapsed = elapsed + Time.deltaTime;
            DashGenerationProgress = elapsed;
            //Debug.Log($"Elapsed Dash Time: {elapsed}");
            yield return null;
        }
        DashGenerationProgress = 1;
        DashCounter = 1;
    }

    [Header("Configuration Object")]
    [SerializeField] private PlayerMovementConfig _playerMovementConfig;
    public PlayerMovementConfig PlayerMovementConfig { get { return _playerMovementConfig; } }
    
    [Header("Player Movement System Dependencies")]
    [SerializeField] private Rigidbody _playerRigidbody;

    private Vector3 forwardVector;
    private Vector3 rightVector;
    private int DashCounter = 1;
    private float DashGenerationProgress = 1f;

}