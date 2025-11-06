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
    private float _health;
    public float MaxHealth = 100f;
    public event Action OnEnemyDamagedEvent;
    public event Action OnEnemyHealedEvent;

    private Color flashColor = Color.red;
    private Color originalColor;
    private float flashDuration = 0.2f;
    private SpriteRenderer enemySprite;

    public void HealEnemy(float heal)
    {
        _health += Mathf.Abs(heal);
        if (_health > MaxHealth) _health = MaxHealth;
        OnEnemyHealedEvent?.Invoke();
    }
    public void DamageEnemy(float damage)
    {
        _health -= Mathf.Abs(damage);
        if (_health < 0)
        {
            //do something
        }
        OnEnemyDamagedEvent?.Invoke();

        StopCoroutine(flashEnemy());
        StartCoroutine(flashEnemy());

        SpawnsDamagePopups.Instance.DamageDone(damage, transform.position);
    }

    IEnumerator flashEnemy()
    {
        enemySprite.material.SetColor("_TintColor", flashColor);
        yield return new WaitForSeconds(flashDuration);
        enemySprite.material.SetColor("_TintColor", originalColor);
    }

    private void Start()
    {
        _health = MaxHealth;

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
}
