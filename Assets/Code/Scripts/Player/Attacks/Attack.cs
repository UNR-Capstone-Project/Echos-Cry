using UnityEngine;

[CreateAssetMenu(menuName = "Combo System/Attack Data")]
public class Attack : ScriptableObject
{
    public enum AttackType
    {
        MELEE = 0,
        PROJECTILE
    }
    public AnimatorOverrideController OverrideAnimation;
    public AnimationClip AnimationClip;
    public float BaseDamage;
    public AttackType TypeOfAttack;
}
