using System;
using UnityEngine;

//This will be placed on every enemy to handle collision with an attack from player

//VERY VERY VERY IMPORTANT NOTE: this will only register collisions from colliders with the layer PlayerAttack placed on them
// so place PlayerAttack on the player's attack handler or whatever handles weapon collision

[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyInfo))]
public class HandleDamageCollision : MonoBehaviour
{
    //This ensures that the PlayerAttack layer will be the only collider that can trigger enemy damage
    void SetupColliderLayers()
    {
        LayerMask includeThisLayer = 1 << LayerMask.NameToLayer("PlayerAttack");
        LayerMask excludeAllLayers = ~0;
        LayerMask excludeThisLayer = excludeAllLayers ^ includeThisLayer;

        _enemyCollider.includeLayers |= includeThisLayer;
        _enemyCollider.excludeLayers |= excludeAllLayers;
        _enemyCollider.excludeLayers &= excludeThisLayer;
    }
    void SetupCollider()
    {
        _enemyCollider.isTrigger = true;
    }

    private void Awake()
    {
        _enemyInfo = GetComponent<EnemyInfo>();
        _enemyStats = GetComponent<EnemyStats>();
    }
    private void Start()
    {
        SetupCollider();
        SetupColliderLayers();
    }
    private void OnTriggerEnter(Collider other)
    {
        //TODO: Implement way to access player's current damage, possibly through static function that could access player's current damage amount?

        float damageAmount = other.GetComponentInParent<BaseAttack>().TotalAttackDamage;

        _enemyStats.DamageEnemy(damageAmount);
        PlayerStats.UpdateComboMeter(1f);

        OnCollisionEvent?.Invoke();
    }

    //TODO: Need way to grab whatever current damage player is doing
    [SerializeField] private CapsuleCollider _enemyCollider;
    [SerializeField] private PlayerStats playerStats;

    private EnemyStats _enemyStats;
    private EnemyInfo _enemyInfo;

    public event Action OnCollisionEvent;
}
