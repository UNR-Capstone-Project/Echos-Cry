using UnityEngine;

[CreateAssetMenu(fileName = "Marked For Death Effect", menuName = "Echo's Cry/Passive Effects/Marked For Death")]
public class MarkedForDeathPassive : PassiveEffect
{
    private bool _markedForDeath = false;
    
    public override void UseEffect()
    {
        if (!_markedForDeath)
        {
            enemyReference.SetDamageMultiplier(1.2f); //20% more damage is dealt on this enemy
            _markedForDeath = true;
        }
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
        if (_markedForDeath)
        {
            enemyReference.SetDamageMultiplier(1f); //Reset damage multiplier
            _markedForDeath = false;
        }
    }
}
