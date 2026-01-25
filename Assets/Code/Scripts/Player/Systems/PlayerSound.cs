using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public void PlayFootstep()
    {
        if (!SoundEffectManager.Instance.Builder.GetSoundPlayer().IsSoundPlaying())
        {
            SoundEffectManager.Instance.Builder
            .SetSound(_playerSoundConfig.FootstepSFX)
            .SetSoundPosition(transform.position)
            .ValidateAndPlaySound();
        }
    }
    public void PlayHitQuality()
    {
        if (TempoConductor.Instance.CurrentHitQuality == TempoConductor.HitQuality.Excellent)
            SoundEffectManager.Instance.Builder
            .SetSound(_playerSoundConfig.ExcellentHitSFX)
            .SetSoundPosition(transform.position)
            .ValidateAndPlaySound();
        else if (TempoConductor.Instance.CurrentHitQuality == TempoConductor.HitQuality.Good)
            SoundEffectManager.Instance.Builder
            .SetSound(_playerSoundConfig.GoodHitSFX)
            .SetSoundPosition(transform.position)
            .ValidateAndPlaySound();
        else
            SoundEffectManager.Instance.Builder
            .SetSound(_playerSoundConfig.MissHitSFX)
            .SetSoundPosition(transform.position)
            .ValidateAndPlaySound();
    }

    public void PlayDash()
    {
        SoundEffectManager.Instance.Builder
        .SetSound(_playerSoundConfig.DashSFX)
        .SetSoundPosition(transform.position)
        .ValidateAndPlaySound();
    }

    private void PlayHurt()
    {
        SoundEffectManager.Instance.Builder
        .SetSound(_playerSoundConfig.HurtEffect)
        .SetSoundPosition(transform.position)
        .ValidateAndPlaySound();
    }

    private void PlayHeal()
    {
        SoundEffectManager.Instance.Builder
        .SetSound(_playerSoundConfig.HealEffect)
        .SetSoundPosition(transform.position)
        .ValidateAndPlaySound();
    }


    [Header("Configuration Object")]
    [SerializeField] private SFXConfig _playerSoundConfig;

}
