using UnityEngine;

//This will be placed on every enemy to handle collision with an attack from player

[RequireComponent (typeof(CapsuleCollider))]
public class HandleDamageCollision : MonoBehaviour
{
    //TODO: Need way to grab whatever current damage player is doing
    private EnemyInfo _enemyInfo;
    private Collider _enemyCollider;

    //This ensures that the PlayerAttack layer will be the only collider that can trigger enemy damage
    void SetupColliderLayers()
    {
        LayerMask includeThisLayer = 1 << LayerMask.NameToLayer("PlayerAttack");
        LayerMask excludeAllLayers = ~0;
        LayerMask excludeThisLayer = excludeAllLayers ^ includeThisLayer;
        _enemyCollider.includeLayers |= includeThisLayer;
        _enemyCollider.excludeLayers |= excludeAllLayers;
        _enemyCollider.excludeLayers &= excludeThisLayer;
        _enemyCollider.isTrigger = true;
    }
    private void Awake()
    {
        _enemyInfo = GetComponent<EnemyInfo>();
        _enemyCollider = GetComponent<Collider>();
    }
    private void Start()
    {
        SetupColliderLayers();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        DamageHandler.Instance.AddToDamageQueue(_enemyInfo.EnemyID, 5f);
    }
}
