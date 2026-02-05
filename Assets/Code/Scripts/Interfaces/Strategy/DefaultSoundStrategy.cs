using AudioSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Strategies/Sound/Default Sound Strategy")]
public class DefaultSoundStrategy : SoundStrategy
{
    public override void Execute(soundEffect sfx, Transform origin, float time)
    {
        SoundEffectManager.Instance.Builder
            .SetSound(sfx)
            .SetSoundPosition(origin.position)
            .SetDelay(time)
            .ValidateAndPlaySound();
    }
}
