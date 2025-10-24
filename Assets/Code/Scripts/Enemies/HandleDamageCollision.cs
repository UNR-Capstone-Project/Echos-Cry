using System;
using UnityEngine;

//This will be placed on every enemy to handle collision with an attack from player

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
        _enemyInfo = GetComponent<EnemyInfo>();
        _enemyCollider = gameObject.AddComponent<CapsuleCollider>();
    }
    private void Start()
    {
        SetupCollider();
        SetupColliderLayers();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        DamageHandler.Instance.AddToDamageQueue(_enemyInfo.EnemyID, 5f);
        OnCollisionEvent?.Invoke();
    }

    //TODO: Need way to grab whatever current damage player is doing
    private EnemyInfo       _enemyInfo;
    private CapsuleCollider _enemyCollider;

    public float ColliderRadius = 0.5f;
    public float ColliderHeight = 2f;

    public event Action OnCollisionEvent;
}
