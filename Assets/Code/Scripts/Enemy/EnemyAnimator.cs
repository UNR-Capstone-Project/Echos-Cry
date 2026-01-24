using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class EnemyAnimator : MonoBehaviour
{
    public void HandleDamageEnemy(float damage, Color color)
    {
        StopCoroutine(FlashEnemy());
        StartCoroutine(FlashEnemy());
        SpawnsDamagePopups.Instance.DamageDone(damage, transform.position, color);
        if (bloodEffect != null) { bloodEffect.Play(); }
    }

    private IEnumerator FlashEnemy()
    {
        enemySprite.material.SetColor(hashedTintColor, flashColor);
        yield return new WaitForSeconds(flashDuration);
        enemySprite.material.SetColor(hashedTintColor, originalColor);
    }

    public void SetMarkedForDeath(bool isMarked)
    {
        if (markedForDeathObject != null)
            markedForDeathObject.SetActive(isMarked);
    }

    private void Start()
    {
        hashedTintColor = Shader.PropertyToID("_TintColor");

        enemySprite = GetComponentInChildren<SpriteRenderer>();
        bloodEffect = GetComponentInChildren<VisualEffect>();
        enemyAgent = GetComponent<NavMeshAgent>();

        if (enemySprite != null)
        {
            originalColor = enemySprite.material.GetColor(hashedTintColor);
        }
        else
        {
            Debug.Log("Must have enemy sprite attached to apply tint.");
        }
        GetComponent<EnemyStats>().OnEnemyDamagedEvent += HandleDamageEnemy;
    }

    private void Update()
    {
        if (enemySprite != null)
        {
            if (Mathf.Abs(enemyAgent.velocity.x) > 0.01f)
            {
                Vector3 scale = transform.localScale;
                scale.x = enemyAgent.velocity.x > 0 ? -1 : 1;
                transform.localScale = scale;
            }
        }
    }

    private void OnDestroy()
    {
        GetComponent<EnemyStats>().OnEnemyDamagedEvent -= HandleDamageEnemy;
    }

    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] private GameObject markedForDeathObject;

    private VisualEffect bloodEffect;
    private Color originalColor;
    private SpriteRenderer enemySprite;
    private NavMeshAgent enemyAgent;
    private int hashedTintColor = Shader.PropertyToID("_TintColor");
}
