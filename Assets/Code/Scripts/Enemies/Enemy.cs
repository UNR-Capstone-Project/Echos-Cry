using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.Windows;

public class Enemy : MonoBehaviour
{
    public float enemyDamage = 10f;
    public float enemyAttackWait = 2f;
    public float enemyHealth = 10f;
    public float enemySpeed = 2f;
    public float detectPlayerRadius = 10f;
    public float knockBackDuration = 0.2f;
    public Color flashColor = Color.red;
    public GameObject attackEffect;

    private GameObject playerTarget;
    private CharacterController characterController;
    private SpriteRenderer enemySprite;

    private float knockForce;
    private Vector3 knockDir;
    private float knockTimer;
    private Color originalColor;
    private float flashDuration = 0.2f;
    private bool isAttacking = false;

    private enum ENEMY_STATE
    {
        ROAM,
        CHASE,
        ATTACK,
        KNOCKBACK
    }

    private ENEMY_STATE currentState = ENEMY_STATE.ROAM;
    private ENEMY_STATE previousState;

    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        if (playerTarget == null)
        {
            Debug.Log("Enemy could not find the Player Game Object.");
        }

        characterController = GetComponent<CharacterController>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        originalColor = enemySprite.material.GetColor("_TintColor");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switchState(ENEMY_STATE.ATTACK);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switchState(ENEMY_STATE.CHASE);
        }
    }

    void Update()
    {
        //Handle player death.
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        //Handle state for chasing the player.
        if (playerTarget != null)
        {
            if ((transform.position - playerTarget.transform.position).magnitude <= detectPlayerRadius
                && currentState == ENEMY_STATE.ROAM) //Player within enemies detection radius.
            {
                switchState(ENEMY_STATE.CHASE);
            }
            else if (currentState == ENEMY_STATE.CHASE) //Player has escaped the detection radius.
            {
                switchState(ENEMY_STATE.ROAM);
            }
        }

        //Handle states.
        switch (currentState)
        {
            case ENEMY_STATE.ROAM:
                EnemyRoam();
                break;
            case ENEMY_STATE.CHASE:
                EnemyChase();
                break;
            case ENEMY_STATE.ATTACK:
                EnemyAttack();
                break;
            case ENEMY_STATE.KNOCKBACK:
                EnemyKnockBack();
                break;
        }
    }

    private void switchState(ENEMY_STATE newState)
    {
        previousState = currentState;
        currentState = newState;
    }

    private void EnemyRoam()
    {
        
    }

    private void EnemyChase()
    {
        if (playerTarget != null)
        {
            Vector3 chaseDir = -(transform.position - playerTarget.transform.position).normalized;

            //Face Enemy
            Vector3 currentScale = enemySprite.transform.localScale;
            if (transform.position.x < playerTarget.transform.position.x)
            {
                currentScale.x = -1 * Mathf.Abs(currentScale.x);
                enemySprite.transform.localScale = currentScale;
            }
            else
            {
                currentScale.x = 1 * Mathf.Abs(currentScale.x);
                enemySprite.transform.localScale = currentScale;
            }

            characterController.Move(chaseDir * enemySpeed * Time.deltaTime);
        }
    }

    private void EnemyAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(attackPlayer());
        }
    }

    IEnumerator attackPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.GetComponent<Player>().takeDamage(enemyDamage);
            GameObject attackEffectInstance = Instantiate(attackEffect, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(enemyAttackWait);

        isAttacking = false;
    }

    private void EnemyKnockBack()
    {
        knockTimer -= Time.deltaTime;

        characterController.Move(knockDir * knockForce * Time.deltaTime);

        if (knockTimer <= 0)
        {
            switchState(previousState);
        }
    }

    public void takeDamage(float damageAmount)
    {
        enemyHealth -= damageAmount;
        StopCoroutine(flashEnemy());
        StartCoroutine(flashEnemy());
    }

    IEnumerator flashEnemy()
    {
        enemySprite.material.SetColor("_TintColor", flashColor);
        yield return new WaitForSeconds(flashDuration);
        enemySprite.material.SetColor("_TintColor", originalColor);
    }

    public void takeKnockBack(Vector3 knockDir, float knockForce)
    {
        this.knockDir = knockDir;
        this.knockForce = knockForce;
        knockTimer = knockBackDuration;
        switchState(ENEMY_STATE.KNOCKBACK);
    }
}
