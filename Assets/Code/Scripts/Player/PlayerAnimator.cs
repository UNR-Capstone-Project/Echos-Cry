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
        if (locomotion != Vector2.zero) playerSpriteAnimator.SetBool(hashedIsRunning, true);
        else playerSpriteAnimator.SetBool(hashedIsRunning, false);
    }

    public void DamagePlayerFlash()
    {
        StopCoroutine(flashPlayerDamaged());
        StartCoroutine(flashPlayerDamaged());
    }
    private IEnumerator flashPlayerDamaged()
    {
        playerSpriteRenderer.material.SetColor(hashedTintColor, spriteDamageColor);
        yield return new WaitForSeconds(flashDamageDuration);
        playerSpriteRenderer.material.SetColor(hashedTintColor, defaultSpriteColor);
    }

    public void HandleDashStartedEmit()
    {
        _dashTrail.emitting = true;
    }
    public void HandleDashEndedEmit()
    {
        _dashTrail.emitting = false;
    }

    private void Awake()
    {
        playerSpriteAnimator  = GetComponent<Animator>();
        playerSpriteRenderer  = GetComponent<SpriteRenderer>();
        playerSpriteTransform = GetComponent<Transform>();
        _dashTrail = GetComponentInParent<TrailRenderer>();
    }
    private void Start()
    {
        defaultSpriteColor = playerSpriteRenderer.material.GetColor(hashedTintColor);
        PlayerStats.OnPlayerDamagedEvent += DamagePlayerFlash;

        PlayerMovement.OnDashStarted += HandleDashStartedEmit;
        PlayerMovement.OnDashEnded += HandleDashEndedEmit;

        _translator.OnMovementEvent += HandleMovement;
    }
    private void OnDestroy()
    {
        PlayerStats.OnPlayerDamagedEvent -= DamagePlayerFlash;

        PlayerMovement.OnDashStarted -= HandleDashStartedEmit;
        PlayerMovement.OnDashEnded   -= HandleDashEndedEmit;

        _translator.OnMovementEvent -= HandleMovement;
    }

    [SerializeField] private InputTranslator _translator;

    [SerializeField] private Color spriteDamageColor = Color.red;
    [SerializeField] private float flashDamageDuration = 0.2f;
    private Color defaultSpriteColor;

    private TrailRenderer _dashTrail = null;

    private Transform      playerSpriteTransform;
    private SpriteRenderer playerSpriteRenderer;
    private Animator       playerSpriteAnimator;

    private int hashedTintColor = Shader.PropertyToID("_TintColor");
    private int hashedIsRunning = Animator.StringToHash("isRunning");
}
