using System.Collections;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public void FlashEnemy()
    {
        StopCoroutine(FlashEnemyCoroutine());
        StartCoroutine(FlashEnemyCoroutine());
    }

    private IEnumerator FlashEnemyCoroutine()
    {
        enemySprite.material.SetColor(hashedTintColor, flashColor);
        yield return new WaitForSeconds(flashDuration);
        enemySprite.material.SetColor(hashedTintColor, originalColor);
    }

    void Start()
    {
        hashedTintColor = Shader.PropertyToID("_TintColor");

        enemySprite = GetComponentInChildren<SpriteRenderer>();
        if (enemySprite != null)
        {
            originalColor = enemySprite.material.GetColor(hashedTintColor);
        }
        else
        {
            Debug.Log("Must have enemy sprite attached to apply tint.");
        }
    }

    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.2f;
    private Color originalColor;
    private SpriteRenderer enemySprite;
    private int hashedTintColor = Shader.PropertyToID("_TintColor");
}
