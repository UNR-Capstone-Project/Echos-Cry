using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class DeathEffectHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathParticles;
    private SpriteRenderer _enemySpriteRenderer;

    public void SetSpriteShape(SpriteRenderer spriteRenderer)
    {
        //_enemySpriteRenderer = spriteRenderer;
        if (spriteRenderer == null)
        {
            Debug.Log("Sprite renderer not set! Cannot display death particles!");
            return;
        }

        var shape = _deathParticles.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Sprite;

        shape.sprite = spriteRenderer.sprite;
        shape.texture = spriteRenderer.sprite.texture;
        shape.scale = spriteRenderer.transform.localScale;
        shape.position = transform.InverseTransformPoint(spriteRenderer.bounds.center);

        StartParticles();
    }

    private void StartParticles()
    {
        _deathParticles.Play();
    }
    private void Update()
    {
        if (!_deathParticles.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
