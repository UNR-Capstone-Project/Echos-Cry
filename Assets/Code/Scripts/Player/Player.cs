using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using static TempoManagerV2;
using UnityEngine.Windows;
using System;

public class Player : MonoBehaviour
{
    //----------Variables----------

    //Player Movement
    private Vector2 playerLocomotion = Vector2.zero;

    [SerializeField] private InputTranslator inputTranslator;
    [SerializeField] private float playerGravity = 9.8f;
    [SerializeField] private float playerSpeed = 10;
    [SerializeField] private Vector3 groundCheckBoxDimensions;
    [SerializeField] private float groundCheckBoxHeight;

    //Player Stats
    private playerHealthController healthController;
    //public float playerHealth = 10f;
    public float attackWait = 0.5f;

    //Player Components
    public GameObject playerSprite;
    public GameObject fireballPrefab;
    public GameObject musicMiniGamePrefab;

    private TempoManagerV2 tempoManager;
    private Transform mainCameraRef;
    private Rigidbody playerRigidbody;
    private SpriteRenderer playerSpriteRenderer;
    private Animator playerAnimator;

    //Player Attacking
    public Color flashColor = Color.red;

    private bool isAttackCooldown = false;
    private Color originalColor;
    private float flashDuration = 0.2f;
    private Quaternion playerRotation;

    //Minigame Variables
    private bool miniGameOpened = false;

    //Events
    //public event Action<float> HealthUpdateEvent;

    //----------End Variables----------

    private void Start()
    {
        //Get Components
        mainCameraRef = Camera.main.transform;
        tempoManager = GameObject.Find("TempoManager").GetComponent<TempoManagerV2>();
        playerAnimator = playerSprite.GetComponent<Animator>();
        playerSpriteRenderer = playerSprite.GetComponent<SpriteRenderer>();
        playerRigidbody = GetComponent<Rigidbody>();
        originalColor = playerSpriteRenderer.material.GetColor("_TintColor");

        //Hook up input signals
        inputTranslator.OnMovementEvent += HandleMovement;
        inputTranslator.OnMousePrimaryInteractionEvent += HandleMousePrimaryInteraction;
        healthController = GetComponent<playerHealthController>();
    }
    private void OnDestroy()
    {
        //Signal cleaup
        inputTranslator.OnMovementEvent -= HandleMovement;
        inputTranslator.OnMousePrimaryInteractionEvent -= HandleMousePrimaryInteraction;
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (!IsGrounded())
        {
            playerRigidbody.AddForce(playerGravity * Vector3.down, ForceMode.Acceleration);
        }
    }

    public bool IsGrounded()
    {
        return Physics.BoxCast(transform.position, groundCheckBoxDimensions, Vector3.down, transform.rotation, groundCheckBoxHeight);
    }
    private void MovePlayer()
    {
        Vector3 forwardVector = mainCameraRef.forward;
        forwardVector.y = 0f;
        forwardVector = forwardVector.normalized;

        Vector3 rightVector = mainCameraRef.right;
        rightVector.y = 0f;
        rightVector = rightVector.normalized;
        
        Vector3 targetVel = (playerLocomotion.y * playerSpeed * forwardVector)
                          + (playerLocomotion.x * playerSpeed * rightVector)
                          + (Vector3.up * playerRigidbody.linearVelocity.y);

        playerRigidbody.AddForce(targetVel - playerRigidbody.linearVelocity, ForceMode.VelocityChange);
    }

    public void HandleMovement(Vector2 locomotion)
    {
        playerLocomotion = locomotion;

        //Flip player
        if (Mathf.Abs(locomotion.x) > 0)
        {
            Vector3 currentScale = playerSprite.transform.localScale;
            currentScale.x = Mathf.Sign(locomotion.x) * Mathf.Abs(currentScale.x);
            playerSprite.transform.localScale = currentScale;
        }

        //Animate player
        if (locomotion.x != 0 || locomotion.y != 0)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
    }

    public void HandleMousePrimaryInteraction()
    {
        tempoManager.UpdateHitQuality();
        switch (tempoManager.currentHitQuality)
        {
            case HIT_QUALITY.EXCELLENT:
                Debug.Log("EXCELLENT");
                break;
            case HIT_QUALITY.GOOD:
                Debug.Log("GOOD");
                break;
            case HIT_QUALITY.BAD:
                Debug.Log("BAD");
                break;
            default:
                Debug.Log("MISS");
                break;
        };

        ProjectileAttack();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (Vector3.down * groundCheckBoxHeight), groundCheckBoxDimensions);
    }

    private void ProjectileAttack()
    {
        if (!isAttackCooldown)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            int groundMask = LayerMask.GetMask("Ground");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
            {
                Vector3 hitPoint = hit.point;
                Vector3 targetVec = hitPoint - transform.position;
                Vector3 projectileDirection = targetVec.normalized;

                if (projectileDirection.y < 0f) { projectileDirection.y = 0f; } //Clamp to angles above 0 degrees horizontally
                projectileDirection.Normalize();

                Vector3 spawnPos = transform.position + projectileDirection * 0.5f;
                GameObject fireballInstance = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);
                fireballInstance.GetComponent<ProjectileAttack>().setProjectileDirection(projectileDirection);

                isAttackCooldown = true;
                StartCoroutine(AttackCooldown(attackWait));
            }
        }
    }
    public void closeMiniGame() { miniGameOpened = false; }

    private void OpenMinigame()
    {
        //ISSUE: Determine type of weapon being used, and open the corresponding mini-game.
        if (!miniGameOpened)
        {
            GameObject musicMiniGameInstance = Instantiate(musicMiniGamePrefab);
            musicMiniGameInstance.name = musicMiniGamePrefab.name;
            miniGameOpened = true;
        }
    }

    IEnumerator AttackCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        isAttackCooldown = false;
    }

    public void takeDamage(float damageAmount)
    {
        if (healthController != null)
        {
            healthController.onDamageTaken(damageAmount);
        }
        //playerHealth -= damageAmount;
        //HealthUpdateEvent?.Invoke(playerHealth);

        StopCoroutine(flashPlayer());
        StartCoroutine(flashPlayer());
    }

    IEnumerator flashPlayer()
    {
        playerSpriteRenderer.material.SetColor("_TintColor", flashColor);
        yield return new WaitForSeconds(flashDuration);
        playerSpriteRenderer.material.SetColor("_TintColor", originalColor);
    }
}
