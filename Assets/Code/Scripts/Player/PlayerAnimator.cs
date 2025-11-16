using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public void HandleMovement(Vector2 locomotion)
    {
        UpdateSpriteDirection(locomotion);
        UpdateSpriteAnimation(locomotion);
    }

    void UpdateSpriteDirection(Vector2 locomotion)
    {
        if (locomotion.x == 0) return;
        Vector3 currentScale = playerSpriteTransform.localScale;
        currentScale.x = Mathf.Sign(locomotion.x) * Mathf.Abs(currentScale.x);
        playerSpriteTransform.localScale = currentScale;
    }
    void UpdateSpriteAnimation(Vector2 locomotion)
    {
        //Animate player
        if (locomotion != Vector2.zero) playerSpriteAnimator.SetBool("isRunning", true);
        else playerSpriteAnimator.SetBool("isRunning", false);
    }

    public void DamagePlayerFlash()
    {
        StopCoroutine(flashPlayerDamaged());
        StartCoroutine(flashPlayerDamaged());
    }
    private IEnumerator flashPlayerDamaged()
    {
        playerSpriteRenderer.material.SetColor("_TintColor", spriteDamageColor);
        yield return new WaitForSeconds(flashDamageDuration);
        playerSpriteRenderer.material.SetColor("_TintColor", defaultSpriteColor);
    }
    private void Awake()
    {
        playerSpriteAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerSpriteTransform = GetComponent<Transform>();
    }
    private void Start()
    {
        defaultSpriteColor = playerSpriteRenderer.material.GetColor("_TintColor");
        PlayerStats.OnPlayerDamagedEvent += DamagePlayerFlash;

        InputTranslator.OnMovementEvent += HandleMovement;
    }
    private void OnDestroy()
    {
        PlayerStats.OnPlayerDamagedEvent -= DamagePlayerFlash;

        InputTranslator.OnMovementEvent -= HandleMovement;
    }
    
    private Color defaultSpriteColor;
    [SerializeField] private Color spriteDamageColor = Color.red;

    [SerializeField] private float flashDamageDuration = 0.2f;

    private Transform playerSpriteTransform;
    private SpriteRenderer playerSpriteRenderer;
    private Animator playerSpriteAnimator;
}
