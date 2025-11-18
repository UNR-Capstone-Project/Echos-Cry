using AudioSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Combo System/Attack Data")]
public class AttackData : ScriptableObject
{
    public AnimatorOverrideController OverrideController;
    public AnimationClip AnimationClip;
    public soundEffect AttackSound;
    public float BaseDamage;
}
