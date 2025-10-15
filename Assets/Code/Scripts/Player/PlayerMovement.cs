using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool IsGrounded()
    {
        return Physics.BoxCast(transform.position, groundCheckBoxDimensions, Vector3.down, transform.rotation, groundCheckBoxHeight);
    }
    private void MovePlayer()
    {
        Vector3 forwardVector = mainCameraRef.forward.normalized;
        forwardVector.y = 0f;

        Vector3 rightVector = mainCameraRef.right.normalized;
        rightVector.y = 0f;

        Vector3 targetVel = (playerLocomotion.y * playerSpeed * forwardVector)
                          + (playerLocomotion.x * playerSpeed * rightVector)
                          + (Vector3.up * playerRigidbody.linearVelocity.y);

        playerRigidbody.AddForce(targetVel - playerRigidbody.linearVelocity, ForceMode.VelocityChange);
    }

    public void HandleMovement(Vector2 locomotion)
    {
        playerLocomotion = locomotion;
    }
    public void HandleDash()
    {
        if (!canDash) return;
        playerRigidbody.AddForce(playerRigidbody.linearVelocity.normalized * dashSpeed, ForceMode.Impulse);
        StartCoroutine(DashDurationTimer(dashDuration));
        StartCoroutine(DashCooldownTimer(dashCooldown));
    }

    IEnumerator DashCooldownTimer(float duration)
    {
        canDash = false;
        yield return new WaitForSeconds(duration);
        canDash = true;
    }
    IEnumerator DashDurationTimer(float duration)
    {
        isDashing = true;
        mTrail.emitting = true;
        yield return new WaitForSeconds(duration);
        isDashing = false;
        mTrail.emitting = false;
    }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        mTrail = GetComponent<TrailRenderer>();
        mainCameraRef = Camera.main.transform;
    }
    void Start()
    {
        if(inputTranslator == null) return;
        inputTranslator.OnMovementEvent += HandleMovement;
        inputTranslator.OnDashEvent += HandleDash;
    }
    private void OnDestroy()
    {
        if (inputTranslator == null) return;
        inputTranslator.OnMovementEvent -= HandleMovement;
        inputTranslator.OnDashEvent -= HandleDash;
    }

    private void FixedUpdate()
    {
        if (isDashing) return;
        MovePlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (Vector3.down * groundCheckBoxHeight), groundCheckBoxDimensions);
    }

    //Player Movement
    private Vector2 playerLocomotion = Vector2.zero;
    private Transform mainCameraRef;
    private Rigidbody playerRigidbody;

    //Player Dashing
    private bool canDash = true;
    private bool isDashing = false;
    private TrailRenderer mTrail = null;
    [Header("Determines how quickly dash reaches destination.")]
    [SerializeField] private float dashSpeed = 20f;
    [Header("Determines the distance of the dash.")]
    [SerializeField] private float dashDuration = 3f;
    [Header("")]
    [SerializeField] private float dashCooldown = 1f;

    [SerializeField] private InputTranslator inputTranslator;
    //[SerializeField] private float playerGravity = 9.8f; Rigidbody has implementation for gravity and mass
    [SerializeField] private float playerSpeed = 10f;
    
    [SerializeField] private Vector3 groundCheckBoxDimensions;
    [SerializeField] private float groundCheckBoxHeight;
}
