using AudioSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//Handles enemy stats such as health, armor etc
//Contains relevant functions and events for each stat/event

[RequireComponent(typeof(EnemyInfo))]
[RequireComponent(typeof(HandleDamageCollision))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyStats : MonoBehaviour
{
    public float Health { get; private set; }
    public float MaxHealth = 100f;
    public event Action OnEnemyDamagedEvent;
    public event Action OnEnemyHealedEvent;
    public event Action OnEnemyDeathEvent;

    [SerializeField] soundEffect hitSFX;

    private Color flashColor = Color.red;
    private Color originalColor;
    private float flashDuration = 0.2f;
    private SpriteRenderer enemySprite;

    [System.Serializable]
    public struct ItemDrop
    {
        public GameObject prefab;
        public int amount;
    }

    [SerializeField] private ItemDrop[] ItemDrops;
 
    public void HealEnemy(float heal)
    {
        Health += Mathf.Abs(heal);
        if (Health > MaxHealth) Health = MaxHealth;
        OnEnemyHealedEvent?.Invoke();
    }
    public void DamageEnemy(float damage)
    {
        Health -= Mathf.Abs(damage);
        OnEnemyDamagedEvent?.Invoke();

        StopCoroutine(flashEnemy());
        StartCoroutine(flashEnemy());

        SpawnsDamagePopups.Instance.DamageDone(damage, transform.position);
        soundEffectManager.Instance.Builder
                    .setSound(hitSFX)
                    .setSoundPosition(transform.position)
                    .ValidateAndPlaySound();
    }

    IEnumerator flashEnemy()
    {
        enemySprite.material.SetColor("_TintColor", flashColor);
        yield return new WaitForSeconds(flashDuration);
        enemySprite.material.SetColor("_TintColor", originalColor);
    }

    private void Start()
    {
        Health = MaxHealth;

        enemySprite = GetComponentInChildren<SpriteRenderer>();
        if (enemySprite != null)
        {
            originalColor = enemySprite.material.GetColor("_TintColor");
        }
        else
        {
            Debug.Log("Must have enemy sprite attached to apply tint.");
        }
    }

    public void KillEnemy()
    {
        foreach (var itemDrop in ItemDrops)
        {
            for (int i = 0; i < itemDrop.amount; i++)
            {
                GameObject itemInstance = Instantiate(itemDrop.prefab, transform.position, Quaternion.identity);
                Rigidbody itemRB = itemInstance.GetComponent<Rigidbody>();
                if (itemRB != null)
                {
                    Vector3 randDirection = UnityEngine.Random.onUnitSphere;
                    float itemImpulseSpeed = 2f;
                    itemRB.AddForce(randDirection * itemImpulseSpeed, ForceMode.Impulse);
                }
            }
        }
        OnEnemyDeathEvent?.Invoke();
        Destroy(gameObject);
    }
}
