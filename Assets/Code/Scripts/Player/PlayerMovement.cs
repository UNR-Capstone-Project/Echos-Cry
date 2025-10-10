using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool IsGrounded()
    {
        return Physics.BoxCast(transform.position, groundCheckBoxDimensions, Vector3.down, transform.rotation, groundCheckBoxHeight);
    }
    private void MovePlayer()
    {
        dashDirection = playerLocomotion;

        Vector3 forwardVector = mainCameraRef.forward.normalized;
        forwardVector.y = 0f;

        Vector3 rightVector = mainCameraRef.right.normalized;
        rightVector.y = 0f;

        Vector3 targetVel = (playerLocomotion.y * playerSpeed * forwardVector)
                          + (playerLocomotion.x * playerSpeed * rightVector)
                          + (Vector3.up * playerRigidbody.linearVelocity.y);

        playerRigidbody.AddForce(targetVel - playerRigidbody.linearVelocity, ForceMode.VelocityChange);
    }
    
    private void DashPlayer()
    {
        Vector3 forwardVector = mainCameraRef.forward.normalized;
        forwardVector.y = 0f;

        Vector3 rightVector = mainCameraRef.right.normalized;
        rightVector.y = 0f;

        Vector3 targetVel = (dashDirection.y * dashSpeed * forwardVector)
                          + (dashDirection.x * dashSpeed * rightVector)
                          + (Vector3.up * playerRigidbody.linearVelocity.y);

        playerRigidbody.AddForce(targetVel - playerRigidbody.linearVelocity, ForceMode.VelocityChange);

        dashTimer += Time.deltaTime;
        if (dashTimer >= dashDuration)
        {
            dashTimer = 0f;
            StartCoroutine(DashCooldownTimer(dashCooldown));
            currentMoveState = MOVE_STATE.NORMAL;
        }
    }
    IEnumerator DashCooldownTimer(float duration)
    {
        canDash = false;
        yield return new WaitForSeconds(duration);
        canDash = true;
    }

    public void HandleMovement(Vector2 locomotion)
    {
        playerLocomotion = locomotion;
    }
    public void HandleDash()
    {
        if (canDash)
        {
            if (Mathf.Abs(playerLocomotion.x) > 0 || Mathf.Abs(playerLocomotion.y) > 0)
            {
                currentMoveState = MOVE_STATE.DASH;
            }
        }
    }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
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
    }
    private void FixedUpdate()
    {
        switch (currentMoveState)
        {
            case MOVE_STATE.NORMAL:
                MovePlayer();
                break;

            case MOVE_STATE.DASH:
                DashPlayer();
                break;
        }

        mTrail.emitting = currentMoveState == MOVE_STATE.DASH;
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
    private Vector2 dashDirection = Vector2.zero;
    private float dashTimer = 0f;
    private bool canDash = true;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 3f;
    [SerializeField] private float dashCooldown = 5f;
    [SerializeField] private TrailRenderer mTrail;


    [SerializeField] private InputTranslator inputTranslator;
    //[SerializeField] private float playerGravity = 9.8f; Rigidbody has implementation for gravity and mass
    [SerializeField] private float playerSpeed = 10f;
    
    [SerializeField] private Vector3 groundCheckBoxDimensions;
    [SerializeField] private float groundCheckBoxHeight;

    private enum MOVE_STATE
    {
        NORMAL,
        DASH
    }

    private MOVE_STATE currentMoveState = MOVE_STATE.NORMAL;
}
