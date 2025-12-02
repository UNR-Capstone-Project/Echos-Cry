using UnityEngine;

public class BatCollisionHandler: BaseEnemyCollisionHandler
{
    protected override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
        _enemyManager.SwitchState(SimpleEnemyStateCache.EnemyStates.BAT_STAGGER);
    }
}
