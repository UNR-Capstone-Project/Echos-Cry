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
        Vector3 currentScale = _playerSpriteTransform.localScale;
        currentScale.x = Mathf.Sign(locomotion.x) * Mathf.Abs(currentScale.x);
        _playerSpriteTransform.localScale = currentScale;
    }
    void UpdateSpriteAnimation(Vector2 locomotion)
    {
        //Animate player
        if (locomotion != Vector2.zero) _playerSpriteAnimator.SetBool(hashedIsRunning, true);
        else _playerSpriteAnimator.SetBool(hashedIsRunning, false);
    }

    public void PlayAttackAnimation(ComboStateMachine.StateName state)
    {
        _playerSpriteAnimator.Play("Attack");
    }

    public void DamagePlayerFlash()
    {
        StopCoroutine(flashPlayerDamaged());
        StartCoroutine(flashPlayerDamaged());
    }
    private IEnumerator flashPlayerDamaged()
    {
        _playerSpriteRenderer.material.SetColor(hashedTintColor, spriteDamageColor);
        yield return new WaitForSeconds(flashDamageDuration);
        _playerSpriteRenderer.material.SetColor(hashedTintColor, defaultSpriteColor);
    }

    public void HandleDashStartedEmit()
    {
        _playerSpriteAnimator.Play("Dash");
        _dashTrail.emitting = true;
    }
    public void HandleDashEndedEmit()
    {
        StartCoroutine(StopDashTrailEmit());
    }

    private IEnumerator StopDashTrailEmit()
    {
        yield return new WaitForSeconds(0.2f);
        _dashTrail.emitting = false;
    }

    private void Awake()
    {
        _playerSpriteAnimator  = GetComponent<Animator>();
        _playerSpriteTransform = GetComponent<Transform>();
        _dashTrail = GetComponent<TrailRenderer>();
    }
    private void Start()
    {
        defaultSpriteColor = _playerSpriteRenderer.material.GetColor(hashedTintColor);

        PlayerStats.OnPlayerDamagedEvent += DamagePlayerFlash;
        BaseWeapon.OnAttackStartEvent += PlayAttackAnimation;
        PlayerMovement.OnDashStarted += HandleDashStartedEmit;
        PlayerMovement.OnDashEnded += HandleDashEndedEmit;

        _translator.OnMovementEvent += HandleMovement;
    }
    private void OnDestroy()
    {
        PlayerStats.OnPlayerDamagedEvent -= DamagePlayerFlash;
        BaseWeapon.OnAttackStartEvent -= PlayAttackAnimation;
        PlayerMovement.OnDashStarted -= HandleDashStartedEmit;
        PlayerMovement.OnDashEnded   -= HandleDashEndedEmit;

        _translator.OnMovementEvent -= HandleMovement;
    }

    [SerializeField] private InputTranslator _translator;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private Color spriteDamageColor = Color.red;
    [SerializeField] private float flashDamageDuration = 0.2f;
    private Color defaultSpriteColor;

    private TrailRenderer _dashTrail = null;

    private Transform      _playerSpriteTransform;
    private Animator       _playerSpriteAnimator;

    private int hashedTintColor = Shader.PropertyToID("_TintColor");
    private int hashedIsRunning = Animator.StringToHash("isRunning");
}
