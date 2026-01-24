using AudioSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Strategies/Sound/Default Sound Strategy")]
public class DefaultSoundStrategy : SoundStrategy
{
    public override void Execute(soundEffect sfx, Transform origin)
    {
        SoundEffectManager.Instance.Builder
            .setSound(sfx)
            .setSoundPosition(origin.position)
            .ValidateAndPlaySound();
    }
}
