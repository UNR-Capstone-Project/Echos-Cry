using System;
using System.Collections;
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
        _enemyManager = GetComponent<SimpleEnemyManager>();
    }
    private void Start()
    {
        SetupCollider();
        SetupColliderLayers();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_canBeDamaged) return;
        _canBeDamaged = false;
        StartCoroutine(DamageCooldown());
        _enemyStats.DamageEnemy(Weapon.CurrentDamage);
        _enemyManager.EnemyStateMachine.HandleSwitchState(SimpleEnemyStateCache.RequestState(SimpleEnemyStateCache.States.BAT_STAGGER));
        PlayerStats.UpdateComboMeter(1f);

        OnCollisionEvent?.Invoke();
    }
    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(_damageCooldown);
        _canBeDamaged = true;
    }

    [SerializeField] private CapsuleCollider _enemyCollider;
    [SerializeField] private float _damageCooldown = 0.5f;

    private EnemyStats _enemyStats;
    private EnemyInfo _enemyInfo;
    private SimpleEnemyManager _enemyManager;
    private bool _canBeDamaged = true;

    public event Action OnCollisionEvent;
}
