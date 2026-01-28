using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class NPCAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _npcSprite;
    [SerializeField] Animator _animator;
    [SerializeField] private VisualEffect _visualEffect;
    [SerializeField] private Transform _spriteTransform;

    private Color _defaultTintColor;
    private readonly int hashedTintColor = Shader.PropertyToID("_TintColor");
 
    public void TintFlash(Color tintColor, float flashDuration)
    {
        if (_npcSprite == null) return;
        StopCoroutine(TintFlashCoroutine(tintColor, flashDuration));
        StartCoroutine(TintFlashCoroutine(tintColor, flashDuration));
    }

    public void UpdateSpriteDirection(Vector3 locomotion, bool isReversed)
    {
        if (locomotion.x == 0) return;
        if (_spriteTransform == null) return;

        Vector3 currentScale = _spriteTransform.localScale;
        currentScale.x = Mathf.Sign(locomotion.x) * Mathf.Abs(currentScale.x);
        if (isReversed) currentScale.x = -currentScale.x;
        _spriteTransform.localScale = currentScale;
    }
    public void PlayAnimation(int hashCode)
    {
        if (_animator == null) return;
        _animator.Play(hashCode);
    }

    public void PlayVisualEffect()
    {
        if (_visualEffect == null) return;
        _visualEffect.Play();
    }

    private IEnumerator TintFlashCoroutine(Color tintColor, float flashDuration)
    {
        _npcSprite.material.SetColor(hashedTintColor, tintColor);
        yield return new WaitForSeconds(flashDuration);
        _npcSprite.material.SetColor(hashedTintColor, _defaultTintColor);
    }

    void Start()
    {
        if (_npcSprite != null)
            _defaultTintColor = _npcSprite.material.GetColor(hashedTintColor);
    }
}