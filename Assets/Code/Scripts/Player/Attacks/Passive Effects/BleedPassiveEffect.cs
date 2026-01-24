using UnityEngine;

[CreateAssetMenu(fileName = "Bleed Effect", menuName = "Echo's Cry/Passive Effects")]
public class BleedPassiveEffect : PassiveEffect
{
    public float bleedDamage = 1f;
    public override void UseEffect()
    {
        enemyReference.DamageEnemy(bleedDamage, Color.red);
    }
}