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

    private void Start()
    {
        _builderRef = soundEffectManager.Instance.Builder;
    }

    [Header("Configuration Object")]
    [SerializeField] private PlayerSoundConfig _playerSoundConfig;

    private soundBuilder _builderRef;
}
