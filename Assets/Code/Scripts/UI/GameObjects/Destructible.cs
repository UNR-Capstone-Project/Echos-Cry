using AudioSystem;
using UnityEngine;

public class Destructible : EnemyDrops
{
    private soundBuilder _builderRef;
    [SerializeField] private bool hasItemDrops = true;
    [SerializeField] private GameObject destroyedVersion;
    [SerializeField] soundEffect destroyEffect;

    //protected void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.TryGetComponent<AttackCollisionHandler>(out AttackCollisionHandler handler))
    //    {
    //        _builderRef
    //        .setSound(destroyEffect)
    //        .setSoundPosition(transform.position)
    //        .ValidateAndPlaySound();

    //        Instantiate(destroyedVersion, transform.position, transform.rotation);
    //        if (hasItemDrops) { HandleEnemyDrops(); }
    //        Destroy(gameObject);
    //    }
    //}

    //public override void Start()
    //{
    //    _builderRef = soundEffectManager.Instance.Builder;
    //    //Override enemy behaviors
    //}
    //public override void OnDestroy()
    //{
    //    //Override enemy behaviors
    //}
}
