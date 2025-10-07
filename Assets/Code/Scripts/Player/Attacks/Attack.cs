using UnityEngine;

public class Attack : ScriptableObject
{
    public enum AttackType
    {
        MELEE = 0,
        PROJECTILE
    }
    public AnimatorOverrideController OverrideAnimation;
    public float BaseDamage;
    public AttackType TypeOfAttack;
}
