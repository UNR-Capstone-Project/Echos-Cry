using UnityEngine;

[CreateAssetMenu(fileName = "Bleed Effect", menuName = "Echo's Cry/Passive Effects/Bleed")]
public class BleedPassiveEffect : PassiveEffect
{
    public float bleedDamage = 1f;
    public override void UseEffect()
    {
        enemyManager.Stats.Damage(bleedDamage, Color.red);
    }
}