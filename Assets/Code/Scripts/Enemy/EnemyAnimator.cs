using System.Collections;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public void HandleDamageEnemy(float damage)
    {
        StopCoroutine(FlashEnemy());
        StartCoroutine(FlashEnemy());
        SpawnsDamagePopups.Instance.DamageDone(damage, transform.position);
    }

    private IEnumerator FlashEnemy()
    {
        enemySprite.material.SetColor("_TintColor", flashColor);
        yield return new WaitForSeconds(flashDuration);
        enemySprite.material.SetColor("_TintColor", originalColor);
    }

    void Start()
    {
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        if (enemySprite != null)
        {
            originalColor = enemySprite.material.GetColor("_TintColor");
        }
        else
        {
            Debug.Log("Must have enemy sprite attached to apply tint.");
        }
        GetComponent<EnemyStats>().OnEnemyDamagedEvent += HandleDamageEnemy;
    }
    private void OnDestroy()
    {
        GetComponent<EnemyStats>().OnEnemyDamagedEvent -= HandleDamageEnemy;
    }

    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.2f;
    private Color originalColor;
    private SpriteRenderer enemySprite;
}
