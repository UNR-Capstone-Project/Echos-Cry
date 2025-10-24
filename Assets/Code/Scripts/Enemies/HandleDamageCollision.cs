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
        _enemyCollider.height = ColliderHeight;
        _enemyCollider.radius = ColliderRadius;
    }

    private void Awake()
    {
        _enemyInfo     = GetComponent<EnemyInfo>();
        _enemyStats    = GetComponent<EnemyStats>();
        _enemyCollider = gameObject.AddComponent<CapsuleCollider>();
    }
    private void Start()
    {
        SetupCollider();
        SetupColliderLayers();
    }
    private void OnTriggerEnter(Collider other)
    {
        //TODO: Implement way to access player's current damage, possibly through static function that could access player's current damage amount?
        _enemyStats.DamageEnemy(15f);
        OnCollisionEvent?.Invoke();
    }

    //TODO: Need way to grab whatever current damage player is doing
    private EnemyInfo       _enemyInfo;
    private CapsuleCollider _enemyCollider;
    private EnemyStats      _enemyStats;

    public float ColliderRadius = 0.5f;
    public float ColliderHeight = 2f;

    public event Action OnCollisionEvent;
}
