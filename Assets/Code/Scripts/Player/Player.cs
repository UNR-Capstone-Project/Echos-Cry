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
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private Vector3 groundCheckBoxDimensions;
    [SerializeField] private float groundCheckBoxHeight;

    //Player Stats

    private playerHealthController healthController;
    public float attackWait = 0.5f;

    public float playerHealth = 10f;
    public Color flashColor = Color.red;

    private Color originalColor;
    private float flashDuration = 0.2f;


    //Player Components
    public GameObject playerSprite;
    public GameObject attackPrefab;

    private TempoManagerV2 tempoManager;
    private Transform mainCameraRef;
    private Rigidbody playerRigidbody;
    private SpriteRenderer playerSpriteRenderer;
    private Animator playerAnimator;

    //Player Attacking
    private float attackWait = 0f;
    private float damageMultiplier = 1f;
    private bool isAttackCooldown = false;

    //Minigame Variables
    //private bool miniGameOpened = false;
    //public GameObject musicMiniGamePrefab;

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

        //Hook up input events
        inputTranslator.OnMovementEvent += HandleMovement;
        inputTranslator.OnMousePrimaryInteractionEvent += HandleMousePrimaryInteraction;
        //healthController = GetComponent<playerHealthController>();
        healthController = gameObject.AddComponent<playerHealthController>();
        Debug.Log("Health Controller found? " + (healthController != null));
    }
    private void OnDestroy()
    {
        //Event cleanup
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
                damageMultiplier = 1.5f;
                break;
            case HIT_QUALITY.GOOD:
                damageMultiplier = 1.2f;
                break;
            case HIT_QUALITY.BAD:
                damageMultiplier = 1.1f;
                break;
            default: //Miss
                damageMultiplier = 1.0f;
                break;
        };

        Attack();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (Vector3.down * groundCheckBoxHeight), groundCheckBoxDimensions);
    }

    private void Attack()
    {
        BaseAttack baseAttack = attackPrefab.GetComponent<BaseAttack>();
        attackWait = baseAttack.GetAttackWait();

        if (!isAttackCooldown)
        {
            GameObject attackInstance = Instantiate(attackPrefab, transform.position, Quaternion.identity);
            attackInstance.GetComponent<BaseAttack>().StartAttack(damageMultiplier);

            //Init attack cooldown
            isAttackCooldown = true;
            damageMultiplier = 1f; //Reset multiplier
            StartCoroutine(AttackCooldown(attackWait));
        }
    }

    /*
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
    */

    IEnumerator AttackCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        isAttackCooldown = false;
    }

    public float getHealth()
    {
        return playerHealth;
    }

    public void takeDamage(float damageAmount)
    {
        if (healthController != null)
        {
            healthController.onDamageTaken(damageAmount);
        }

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
