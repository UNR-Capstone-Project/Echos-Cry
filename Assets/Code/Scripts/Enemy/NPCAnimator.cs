using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class NPCAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _npcSprite;
    [SerializeField] Animator _animator;
    [SerializeField] private VisualEffect _visualEffect;

    private Color _defaultTintColor;
    private readonly int hashedTintColor = Shader.PropertyToID("_TintColor");
 
    public void TintFlash(Color tintColor, float flashDuration)
    {
        StopCoroutine(TintFlashCoroutine(tintColor, flashDuration));
        StartCoroutine(TintFlashCoroutine(tintColor, flashDuration));
    }

    public void PlayAnimation(int hashCode)
    {
        _animator.Play(hashCode);
    }

    public void PlayVisualEffect()
    {
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
        _defaultTintColor = _npcSprite.material.GetColor(hashedTintColor);
    }
}
