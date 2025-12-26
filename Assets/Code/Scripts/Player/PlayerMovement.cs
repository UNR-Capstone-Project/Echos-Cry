using AudioSystem;
using System.Collections;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private void MovePlayer()
    {
        Vector3 forwardVector = mainCameraRef.forward.normalized;
        forwardVector.y = 0f;

        Vector3 rightVector = mainCameraRef.right.normalized;
        rightVector.y = 0f;

        Vector3 targetVel = (playerLocomotion.y * playerSpeed * forwardVector)
                          + (playerLocomotion.x * playerSpeed * rightVector)
                          + new Vector3(0f,playerRigidbody.linearVelocity.y,0f);

        if (playerLocomotion != Vector2.zero) playerRigidbody.AddForce(targetVel - playerRigidbody.linearVelocity, ForceMode.VelocityChange);
        else playerRigidbody.AddForce(-(stoppingAcceleration * playerRigidbody.linearVelocity.normalized));
    }

    public void HandleDash()
    {
        if (!canDash 
            || TempoManager.CurrentHitQuality == TempoManager.HIT_QUALITY.MISS 
            || playerLocomotion == Vector2.zero) return;

        canDash = false;
        playerRigidbody.AddForce(playerRigidbody.linearVelocity.normalized * dashSpeed, ForceMode.Impulse);
        OnDashStarted?.Invoke();

        StartCoroutine(DashDurationTimer(dashDuration));
    }
    IEnumerator DashDurationTimer(float duration)
    {
        isDashing = true;
        _playerCollider.enabled = false; 
        yield return new WaitForSeconds(duration);
        isDashing = false;
        _playerCollider.enabled = true;
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

    private void FixedUpdate()
    {
        if (isDashing || isAttacking) return;
        MovePlayer();
    }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<Collider>();
    }
    void Start()
    {
        mainCameraRef = Camera.main.transform;
        InputTranslator.OnMovementEvent += HandleMovement;
        InputTranslator.OnDashEvent += HandleDash;

        BaseWeapon.OnAttackStartEvent += HandleAttackStart;
        BaseWeapon.OnAttackEndedEvent += HandleAttackEnd;

        stoppingAcceleration = playerSpeed * 2;
        //maxPlayerVelocitySqrMag = maxPlayerVelocityMag * maxPlayerVelocityMag;
    }
    private void OnDestroy()
    {
        InputTranslator.OnMovementEvent -= HandleMovement;
        InputTranslator.OnDashEvent -= HandleDash;
        BaseWeapon.OnAttackStartEvent -= HandleAttackStart;
        BaseWeapon.OnAttackEndedEvent -= HandleAttackEnd;
    }

    //Player Movement
    private static Vector2 playerLocomotion = Vector2.zero;
    public static Vector2 PlayerLocomotion { get { return playerLocomotion; } }

    private static Rigidbody playerRigidbody;
    public static Rigidbody PlayerRigidbody { get { return playerRigidbody; } }
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

    [SerializeField] private float playerSpeed = 10f;
    private float stoppingAcceleration;

    //[SerializeField] private float maxPlayerVelocityMag = 5f;
    //private float maxPlayerVelocitySqrMag;

}
