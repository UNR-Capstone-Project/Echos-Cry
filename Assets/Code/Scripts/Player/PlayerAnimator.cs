using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private void Start()
    {
        PlayerStats.OnPlayerDamagedEvent += OnPlayerDamagedTintFlash;

        if(_playerMainSpriteRenderer == null)
        {
            Debug.LogWarning("Main Player Sprite Renderer not assigned!");
            return;
        }
        defaultSpriteColor = _playerMainSpriteRenderer.material.GetColor(hashedTintColor);
    }
    private void OnDestroy()
    {
        PlayerStats.OnPlayerDamagedEvent -= OnPlayerDamagedTintFlash;
    }

    public void UpdateMainSpriteDirection(Vector2 locomotion)
    {
        if (locomotion.x == 0) return;
        Vector3 currentScale = _playerMainSpriteTransform.localScale;
        currentScale.x = Mathf.Sign(locomotion.x) * Mathf.Abs(currentScale.x);
        _playerMainSpriteTransform.localScale = currentScale;
    }

    public void SetIsMainSpriteRunningAnimation(bool isRunning)
    {
        _playerMainSpriteAnimator.SetBool(hashedIsRunning, isRunning);
    }

    public void OnPlayerDamagedTintFlash()
    {
        StopCoroutine(OnPlayerDamagedTintFlashCoroutine());
        StartCoroutine(OnPlayerDamagedTintFlashCoroutine());
    }
    private IEnumerator OnPlayerDamagedTintFlashCoroutine()
    {
        _playerMainSpriteRenderer.material.SetColor(hashedTintColor, _playerAnimatorConfig.OnPlayerDamagedTintColor);
        yield return new WaitForSeconds(_playerAnimatorConfig.OnPlayerDamagedTintFlashDuration);
        _playerMainSpriteRenderer.material.SetColor(hashedTintColor, defaultSpriteColor);
    }

    public void StartDashTrailEmit()
    {
        _dashTrail.emitting = true;
    }
    public void EndDashTrailEmit()
    {
        _dashTrail.emitting = false;
    }

    [Header("Configuration Object")]
    [SerializeField] private PlayerAnimatorConfig _playerAnimatorConfig;
    [Header("Animator System Dependencies")]
    [SerializeField] private TrailRenderer  _dashTrail;
    [SerializeField] private Transform      _playerMainSpriteTransform;
    [SerializeField] private SpriteRenderer _playerMainSpriteRenderer;
    [SerializeField] private Animator       _playerMainSpriteAnimator;

    private Color defaultSpriteColor;

    private readonly int hashedTintColor = Shader.PropertyToID("_TintColor");
    private readonly int hashedIsRunning = Animator.StringToHash("isRunning");
}