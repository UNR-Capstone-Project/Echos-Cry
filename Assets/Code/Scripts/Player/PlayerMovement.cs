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

        playerRigidbody.AddForce(targetVel);
    }

    public void HandleMovement(Vector2 locomotion)
    {
        playerLocomotion = locomotion;
    }
    public void HandleDash()
    {
        if (!canDash || TempoManager.CurrentHitQuality == TempoManager.HIT_QUALITY.MISS) return;

        canDash = false;
        playerRigidbody.AddForce(playerRigidbody.linearVelocity.normalized * dashSpeed, ForceMode.Impulse);
        OnDashStarted?.Invoke();

        StartCoroutine(DashDurationTimer(dashDuration));
        StartCoroutine(DashCooldownTimer(dashCooldown));
    }

    IEnumerator DashCooldownTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        canDash = true;
    }
    IEnumerator DashDurationTimer(float duration)
    {
        isDashing = true;
        yield return new WaitForSeconds(duration);
        isDashing = false;

        OnDashEnded?.Invoke();
    }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        mainCameraRef = Camera.main.transform;
    }
    void Start()
    {
        InputTranslator.OnMovementEvent += HandleMovement;
        InputTranslator.OnDashEvent += HandleDash;
    }
    private void OnDestroy()
    {
        InputTranslator.OnMovementEvent -= HandleMovement;
        InputTranslator.OnDashEvent -= HandleDash;
    }

    private void FixedUpdate()
    {
        if (isDashing) return;
        MovePlayer();
    }

    //Player Movement
    private static Vector2 playerLocomotion = Vector2.zero;
    public static Vector2 PlayerLocomotion { get { return playerLocomotion; } }

    private static Rigidbody playerRigidbody;
    public static Rigidbody PlayerRigidbody { get { return playerRigidbody; } }

    private Transform mainCameraRef;

    //Player Dashing
    public static event Action OnDashStarted;
    public static event Action OnDashEnded;

    private bool canDash = true;
    private bool isDashing = false;

    [Header("Determines how quickly dash reaches destination.")]
    [SerializeField] private float dashSpeed = 20f;
    [Header("Determines the distance of the dash.")]
    [SerializeField] private float dashDuration = 3f;
    [Header("")]
    [SerializeField] private float dashCooldown = 1f;

    [SerializeField] private float playerSpeed = 10f;

}
