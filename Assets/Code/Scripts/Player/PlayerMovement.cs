using AudioSystem;
using System.Collections;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<Collider>();
    }
    void Start()
    {
        mainCameraRef = Camera.main.transform;
        translator.OnMovementEvent += HandleMovement;
        translator.OnDashEvent += HandleDash;

        BaseWeapon.OnAttackStartEvent += HandleAttackStart;
        BaseWeapon.OnAttackEndedEvent += HandleAttackEnd;

        stoppingAcceleration = playerSpeed * 1.5f;
        //maxPlayerVelocitySqrMag = maxPlayerVelocityMag * maxPlayerVelocityMag;
    }
    private void OnDestroy()
    {
        translator.OnMovementEvent -= HandleMovement;
        translator.OnDashEvent -= HandleDash;
        BaseWeapon.OnAttackStartEvent -= HandleAttackStart;
        BaseWeapon.OnAttackEndedEvent -= HandleAttackEnd;
    }
    private void FixedUpdate()
    {
        if (isDashing || isAttacking) return;
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 forwardVector = mainCameraRef.forward.normalized;
        forwardVector.y = 0f;

        Vector3 rightVector = mainCameraRef.right.normalized;
        rightVector.y = 0f;

        Vector3 targetVel = (playerLocomotion.y * playerSpeed * forwardVector)
                          + (playerLocomotion.x * playerSpeed * rightVector)
                          + new Vector3(0f,_playerRigidbody.linearVelocity.y,0f);

        if (playerLocomotion != Vector2.zero) _playerRigidbody.AddForce(targetVel - _playerRigidbody.linearVelocity, ForceMode.VelocityChange);
        else _playerRigidbody.AddForce(-_playerRigidbody.linearVelocity * stoppingAcceleration, ForceMode.Acceleration);
    }

    public void HandleDash()
    {
        if (!canDash || playerLocomotion == Vector2.zero) return;
        if (dashToBeat)
        {
            if (TempoManager.CurrentHitQuality == TempoManager.HIT_QUALITY.MISS) return;
        }

        canDash = false;

        Vector3 forwardVector = mainCameraRef.forward.normalized;
        forwardVector.y = 0f;

        Vector3 rightVector = mainCameraRef.right.normalized;
        rightVector.y = 0f;

        Vector3 dashDirection =
            (playerLocomotion.y * forwardVector +
             playerLocomotion.x * rightVector).normalized;

        _playerRigidbody.linearVelocity = Vector3.zero;
        _playerRigidbody.AddForce(dashDirection * dashSpeed, ForceMode.VelocityChange);

        OnDashStarted?.Invoke();

        StartCoroutine(DashDurationTimer(dashDuration));
    }
    IEnumerator DashDurationTimer(float duration)
    {
        isDashing = true;

        //Disabled for temporary testing purposes, you can escape the level too easily!
        //_playerCollider.enabled = false; 

        yield return new WaitForSeconds(duration);

        _playerRigidbody.linearVelocity = Vector3.zero;

        isDashing = false;
        //_playerCollider.enabled = true;
        OnDashEnded?.Invoke();

        StartCoroutine(DashCooldownTimer(dashCooldown));
    }
    IEnumerator DashCooldownTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        canDash = true;
    }

    public void HandleMovement(Vector2 locomotion)
    {
        playerLocomotion = locomotion;
    }
    public void HandleAttackStart(ComboStateMachine.StateName state)
    {
        isAttacking = true;
    }
    public void HandleAttackEnd()
    {
        isAttacking = false;
    }


    [SerializeField] private InputTranslator translator;

    //Player Movement
    private static Vector2 playerLocomotion = Vector2.zero;
    public static Vector2 PlayerLocomotion { get { return playerLocomotion; } }

    private static Rigidbody _playerRigidbody;
    public static Rigidbody PlayerRigidbody { get { return _playerRigidbody; } }
    private Collider _playerCollider = null;

    private Transform mainCameraRef;

    //Player Dashing
    public static event Action OnDashStarted;
    public static event Action OnDashEnded;

    private bool canDash = true;
    private bool isDashing = false;
    private bool isAttacking = false;

    [Header("Determines how quickly dash reaches destination.")]
    [SerializeField] private float dashSpeed = 20f;
    [Header("Determines the distance of the dash.")]
    [SerializeField] private float dashDuration = 3f;
    [Header("")]
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private bool dashToBeat = true;

    [SerializeField] private float playerSpeed = 10f;
    private float stoppingAcceleration;
}
