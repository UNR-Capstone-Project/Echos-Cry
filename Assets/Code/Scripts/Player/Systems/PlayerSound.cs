using AudioSystem;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public void PlayFootstep()
    {
        if (!_builderRef.GetSoundPlayer().IsSoundPlaying())
        {
            _builderRef
            .setSound(_playerSoundConfig.FootstepSFX)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
        }
    }
    public void PlayHitQuality()
    {
        if (TempoConductor.Instance.CurrentHitQuality == TempoConductor.HitQuality.Excellent)
            _builderRef
            .setSound(_playerSoundConfig.ExcellentHitSFX)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
        else if (TempoConductor.Instance.CurrentHitQuality == TempoConductor.HitQuality.Good)
            _builderRef
            .setSound(_playerSoundConfig.GoodHitSFX)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
        else
            _builderRef
            .setSound(_playerSoundConfig.MissHitSFX)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
    }

    public void PlayDash()
    {
        _builderRef
        .setSound(_playerSoundConfig.DashSFX)
        .setSoundPosition(transform.position)
        .ValidateAndPlaySound();
    }

    private void Start()
    {
        if(soundEffectManager.Instance == null)
        {
            Debug.LogWarning("Sound Effect Manager builder is null");
            this.enabled = false;
            return;
        }
        _builderRef = soundEffectManager.Instance.Builder;
    }

    [Header("Configuration Object")]
    [SerializeField] private PlayerSoundConfig _playerSoundConfig;

    private soundBuilder _builderRef;
}
