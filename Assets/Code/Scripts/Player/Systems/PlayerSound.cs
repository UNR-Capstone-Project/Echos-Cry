using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public void PlayFootstep()
    {
        if (!SoundEffectManager.Instance.Builder.GetSoundPlayer().IsSoundPlaying())
        {
            SoundEffectManager.Instance.Builder
            .setSound(_playerSoundConfig.FootstepSFX)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
        }
    }
    public void PlayHitQuality()
    {
        if (TempoConductor.Instance.CurrentHitQuality == TempoConductor.HitQuality.Excellent)
            SoundEffectManager.Instance.Builder
            .setSound(_playerSoundConfig.ExcellentHitSFX)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
        else if (TempoConductor.Instance.CurrentHitQuality == TempoConductor.HitQuality.Good)
            SoundEffectManager.Instance.Builder
            .setSound(_playerSoundConfig.GoodHitSFX)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
        else
            SoundEffectManager.Instance.Builder
            .setSound(_playerSoundConfig.MissHitSFX)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
    }

    public void PlayDash()
    {
        SoundEffectManager.Instance.Builder
        .setSound(_playerSoundConfig.DashSFX)
        .setSoundPosition(transform.position)
        .ValidateAndPlaySound();
    }

    private void PlayHurt()
    {
        SoundEffectManager.Instance.Builder
        .setSound(_playerSoundConfig.HurtEffect)
        .setSoundPosition(transform.position)
        .ValidateAndPlaySound();
    }

    private void PlayHeal()
    {
        SoundEffectManager.Instance.Builder
        .setSound(_playerSoundConfig.HealEffect)
        .setSoundPosition(transform.position)
        .ValidateAndPlaySound();
    }


    [Header("Configuration Object")]
    [SerializeField] private SFXConfig _playerSoundConfig;

}
