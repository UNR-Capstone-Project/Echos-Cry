using UnityEngine;

[CreateAssetMenu(fileName = "Bleed Effect", menuName = "Echo's Cry/Passive Effects/Bleed")]
public class BleedPassiveEffect : PassiveEffect
{
    public float bleedDamage = 1f;
    public override void UseEffect()
    {
        enemyReference.Health.Damage(bleedDamage, Color.red);

        enemyReference.SoundStrategy.Execute(enemyReference.SoundConfig.HitSFX, enemyReference.transform, 0);
        enemyReference.NPCAnimator.TintFlash(Color.red, 0.2f);
        enemyReference.NPCAnimator.PlayVisualEffect();

        if (DamageLabelManager.Instance != null)
            DamageLabelManager.Instance.SpawnPopup(bleedDamage, enemyReference.transform.position, Color.red);
    }
}