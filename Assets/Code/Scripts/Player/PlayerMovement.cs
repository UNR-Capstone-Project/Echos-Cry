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

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        mainCameraRef = Camera.main.transform;
    }
    void Start()
    {
        if(inputTranslator == null) return;
        inputTranslator.OnMovementEvent += HandleMovement;
    }
    private void OnDestroy()
    {
        if (inputTranslator == null) return;
        inputTranslator.OnMovementEvent -= HandleMovement;
    }
    private void FixedUpdate()
    {
        MovePlayer();
        if (!IsGrounded()) playerRigidbody.AddForce(Vector3.down * playerGravity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (Vector3.down * groundCheckBoxHeight), groundCheckBoxDimensions);
    }

    //Player Movement
    private Vector2 playerLocomotion = Vector2.zero;
    private Transform mainCameraRef;
    private Rigidbody playerRigidbody;

    [SerializeField] private InputTranslator inputTranslator;
    [SerializeField] private float playerGravity = 9.8f;
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private Vector3 groundCheckBoxDimensions;
    [SerializeField] private float groundCheckBoxHeight;
}
