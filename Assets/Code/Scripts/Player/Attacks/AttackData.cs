using AudioSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Combo System/Attack Data")]
public class AttackData : ScriptableObject
{
    public AnimatorOverrideController OverrideController;
    public AnimationClip              AnimationClip;
    public soundEffect                AttackSound;
    public float                      BaseDamage;
}
