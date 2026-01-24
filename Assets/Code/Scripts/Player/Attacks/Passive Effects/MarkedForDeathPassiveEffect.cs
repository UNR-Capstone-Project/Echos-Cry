using UnityEngine;

[CreateAssetMenu(fileName = "Marked For Death Effect", menuName = "Echo's Cry/Passive Effects/Marked For Death")]
public class MarkedForDeathPassive : PassiveEffect
{
    private bool _markedForDeath = false;
    [SerializeField] private GameObject _markedIcon;
    
    public override void UseEffect()
    {
        if (!_markedForDeath)
        {
            enemyManager.EnemyStats.SetDamageMultiplier(1.2f); //20% more damage is dealt on this enemy
            enemyManager.EnemyAnimatorManager.SetMarkedForDeath(true);
            //enemyReference.Instant
            _markedForDeath = true;
        }
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
        if (_markedForDeath)
        {
            enemyManager.EnemyStats.SetDamageMultiplier(1f); //Reset damage multiplier
            enemyManager.EnemyAnimatorManager.SetMarkedForDeath(false);
            _markedForDeath = false;
        }
    }
}
