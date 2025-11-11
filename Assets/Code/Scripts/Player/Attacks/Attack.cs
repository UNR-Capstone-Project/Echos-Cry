using UnityEngine;

[CreateAssetMenu(menuName = "Combo System/Attack Data")]
public class Attack : ScriptableObject
{
    public enum AttackType
    {
        LIGHT,
        HEAVY
    }

    public AnimatorOverrideController OverrideController;
    public AnimationClip AnimationClip;
    public float BaseDamage;
    public AttackType TypeOfAttack;
}
